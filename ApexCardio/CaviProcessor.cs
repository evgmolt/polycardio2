using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycardio2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public enum BodySide
    {
        Left,
        Right
    }

    

    public static class CaviProcessor
    {
        private const double SamplingRateHz = 125.0; // Частота дискретизации каналов сфигмо и ЭКГ
        private const double BloodDensity = 1.05;    // Плотность крови (г/см³)

        /// <summary>
        /// Вычисляет CAVI для левой или правой стороны на основе объекта DataArrays.
        /// </summary>
        /// <param name="data">Экземпляр DataArrays с заполненными массивами</param>
        /// <param name="side">Сторона измерения (Left или Right)</param>
        /// <param name="patientHeightCm">Рост пациента в сантиметрах (например, 175)</param>
        /// <param name="sbp">Систолическое АД на плече (если 0 — берётся из NIBP1_Syst)</param>
        /// <param name="dbp">Диастолическое АД на плече (если 0 — берётся из NIBP1_Diast)</param>
        /// <param name="validSamplesCount">Количество актуальных отсчётов в массивах (если 0 — берётся длина массива)</param>
        public static CaviMeasurementResult CalculateCavi(
            DataArrays data,
            BodySide side,
            double patientHeightCm,
            int validSamplesCount = 0)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // 1. Определяем давление (по умолчанию берём из NIBP1 — обычно это плечевая манжета)
            double syst = side == BodySide.Left ? data.NIBP1_Syst : data.NIBP3_Syst;
            double diast = side == BodySide.Left ? data.NIBP1_Diast : data.NIBP3_Diast;

            if (syst <= diast || diast <= 0)
                return new CaviMeasurementResult { Side = side };

            // 2. Выбираем каналы сфигмограммы для нужной стороны
            int[] rawHand = side == BodySide.Left ? data.SphigmoLeftHandArray : data.SphigmoRightHandArray;
            int[] rawLeg = side == BodySide.Left ? data.SphigmoLeftLegArray : data.SphigmoRightLegArray;

            int length = validSamplesCount > 0
                ? Math.Min(validSamplesCount, rawHand.Length)
                : rawHand.Length;

            if (length < 250) // Минимум 2 секунды записи при 125 Гц
                return new CaviMeasurementResult { Side = side };

            if (!HasSignalActivity(rawHand, length) || !HasSignalActivity(rawLeg, length))
                return new CaviMeasurementResult { Side = side };

            // 3. Предварительное сглаживание (ФНЧ скользящим средним 3-го порядка для подавления шума АЦП)
            double[] handSmooth = SmoothSignal(rawHand, length);
            double[] legSmooth = SmoothSignal(rawLeg, length);

            // 4. Получаем индексы R-пиков (используем готовый RArray, если он заполнен, иначе ищем по ECG1Array)
            List<int> rPeaks = GetRPeaks(data, length);
            if (rPeaks.Count < 2)
                return new CaviMeasurementResult { Side = side };

            // 5. Анализируем каждый сердечный цикл (R-R интервал)
            var delaysSeconds = new List<double>();
            int ppgWriteIdx = 0;
            for (int i = 0; i < rPeaks.Count - 1; i++)
            {
                int cycleStart = rPeaks[i];
                int cycleEnd = rPeaks[i + 1];

                // Пропускаем слишком короткие (< 0.35с) или слишком длинные (> 2.0с) R-R интервалы (экстрасистолы/артефакты)
                int rrSamples = cycleEnd - cycleStart;
                if (rrSamples < 44 || rrSamples > 250)
                    continue;

                double handFootIdx = FindSubsamplePulseFoot(handSmooth, cycleStart, cycleEnd);
                double legFootIdx = FindSubsamplePulseFoot(legSmooth, cycleStart, cycleEnd);

                if (handFootIdx > cycleStart && legFootIdx > handFootIdx)
                {
                    double deltaT = (legFootIdx - handFootIdx) / SamplingRateHz;

                    // Физиологически корректное окно задержки между плечом и лодыжкой: 30–250 мс
                    if (deltaT >= 0.030 && deltaT <= 0.250)
                    {
                        delaysSeconds.Add(deltaT);
                        if (data.PPGRArray != null && ppgWriteIdx + 1 < data.PPGRArray.Length)
                        {
                            data.PPGRArray[ppgWriteIdx++] = (int)Math.Round(handFootIdx);
//                            data.PPGRArray[ppgWriteIdx++] = (int)Math.Round(legFootIdx);
                        }
                    }
                }
            }

            if (delaysSeconds.Count == 0)
                return new CaviMeasurementResult { Side = side };

            // 6. Убираем выбросы (медианная фильтрация / отсечение крайних 10%) и считаем среднюю задержку
            double cleanMeanDelay = CalculateTrimmedMean(delaysSeconds);

            // 7. Вычисляем эффективную длину пути L (в метрах) по росту пациента
            // Формула Yamashina: L_arm = 0.2195 * H - 2.0734; L_ankle = 0.8129 * H + 12.328 (в см)
            double distanceMeters = CalculateEffectivePathLengthMeters(patientHeightCm);

            // 8. Скорость пульсовой волны (м/с) и итоговый индекс CAVI
            double pwv = distanceMeters / (cleanMeanDelay + 0.045);
            double cavi = ComputeCaviFormula(syst, diast, pwv);

            return new CaviMeasurementResult
            {
                Side = side,
                Cavi = Math.Round(cavi, 2),
                PwvMetersPerSec = Math.Round(pwv, 2),
                MeanDelayMs = Math.Round(cleanMeanDelay * 1000.0, 1),
                ValidBeatsCount = delaysSeconds.Count
            };
        }

        /// <summary>
        /// Расчёт разницы расстояний от сердца до лодыжки и до плеча по росту (формулы Yamashina).
        /// </summary>
        private static double CalculateEffectivePathLengthMeters(double heightCm)
        {
            if (heightCm <= 50 || heightCm > 250)
                throw new ArgumentOutOfRangeException(nameof(heightCm), "Рост должен быть указан в сантиметрах (например, 175).");

            double lAnkleCm = 0.8129 * heightCm + 12.328;
            double lArmCm = 0.2195 * heightCm - 2.0734;
            return (lAnkleCm - lArmCm) / 100.0;
        }

        /// <summary>
        /// Клиническая формула CAVI с кусочно-заданными коэффициентами a и b (Takahashi et al.).
        /// </summary>
        private static double ComputeCaviFormula(double sbp, double dbp, double pwv)
        {
            const double unitConversionFactor = 1050.0 / 133.322;

            double deltaP = sbp - dbp;
            double haBeta = (2.0 * unitConversionFactor / deltaP) * Math.Log(sbp / dbp) * (pwv * pwv);

            double a, b;
            if (haBeta < 7.34875)
            {
                a = 0.850;
                b = 0.695;
            }
            else if (haBeta < 10.30372)
            {
                a = 0.658;
                b = 2.103;
            }
            else
            {
                a = 0.432;
                b = 4.441;
            }

            return a * haBeta + b;
        }

        /// <summary>
        /// Поиск начала подъёма пульсовой волны (foot) с субдискретной точностью (параболическая интерполяция 2-й производной).
        /// </summary>
        private static double FindSubsamplePulseFoot(double[] signal, int startIdx, int endIdx)
        {
            // Находим систолический максимум волны в пределах цикла
            int peakIdx = startIdx;
            double maxVal = double.MinValue;
            for (int i = startIdx + 3; i < endIdx - 2; i++)
            {
                if (signal[i] > maxVal)
                {
                    maxVal = signal[i];
                    peakIdx = i;
                }
            }

            // Окно поиска основания волны: от R-пика до систолического пика
            int searchStart = Math.Max(startIdx + 2, peakIdx - (int)(0.35 * SamplingRateHz));
            int bestIdx = -1;
            double maxD2 = double.MinValue;

            // Ищем максимум второй производной (точка максимального изгиба вверх перед фронтом)
            for (int i = searchStart; i < peakIdx - 1; i++)
            {
                double d2 = signal[i + 1] - 2.0 * signal[i] + signal[i - 1];
                if (d2 > maxD2)
                {
                    maxD2 = d2;
                    bestIdx = i;
                }
            }

            if (bestIdx <= searchStart || bestIdx >= peakIdx - 2)
                return bestIdx;

            // Параболическая интерполяция вокруг bestIdx для устранения дискретности 8 мс (125 Гц)
            double y0 = signal[bestIdx] - 2.0 * signal[bestIdx - 1] + signal[bestIdx - 2];
            double y1 = maxD2;
            double y2 = signal[bestIdx + 2] - 2.0 * signal[bestIdx + 1] + signal[bestIdx];

            double denom = 2.0 * (y0 - 2.0 * y1 + y2);
            if (Math.Abs(denom) > 1e-6)
            {
                double delta = (y0 - y2) / denom;
                if (Math.Abs(delta) <= 1.0)
                    return bestIdx + delta;
            }

            return bestIdx;
        }

        /// <summary>
        /// Извлекает R-пики из готового data.RArray или вычисляет их по data.ECG1Array.
        /// </summary>
        private static List<int> GetRPeaks(DataArrays data, int maxLength)
        {
            var peaks = new List<int>();

            // Если ваш алгоритм уже разметил R-пики в RArray
            if (data.RArray != null && data.RInd > 1)
            {
                int count = Math.Min(data.RInd, data.RArray.Length);
                for (int i = 0; i < count; i++)
                {
                    if (data.RArray[i] > 0 && data.RArray[i] < maxLength)
                        peaks.Add(data.RArray[i]);
                }
                if (peaks.Count >= 2)
                    return peaks;
            }

            // Резервный поиск R-пиков по каналу ECG1Array
            peaks.Clear();
            double sum = 0;
            double max = double.MinValue;
            for (int i = 0; i < maxLength; i++)
            {
                double v = data.ECG1Array[i];
                sum += v;
                if (v > max) max = v;
            }

            double mean = sum / maxLength;
            double threshold = mean + (max - mean) * 0.55;
            int refractorySamples = (int)(0.30 * SamplingRateHz); // 300 мс (37 отсчётов)

            for (int i = 1; i < maxLength - 1; i++)
            {
                int val = data.ECG1Array[i];
                if (val > threshold && val >= data.ECG1Array[i - 1] && val > data.ECG1Array[i + 1])
                {
                    if (peaks.Count == 0 || (i - peaks[^1]) > refractorySamples)
                    {
                        peaks.Add(i);
                    }
                    else if (val > data.ECG1Array[peaks[^1]])
                    {
                        peaks[^1] = i;
                    }
                }
            }

            return peaks;
        }

        /// <summary>



        /// Лёгкое сглаживание (взвешенное окно [0.25, 0.5, 0.25]), не смещающее фазу сигнала.
        /// 
        private static double[] SmoothSignal(int[] source, int length)
        {
            double[] result = new double[length];
            result[0] = source[0];
            result[length - 1] = source[length - 1];
            for (int i = 1; i < length - 1; i++)
            {
                result[i] = 0.25 * source[i - 1] + 0.50 * source[i] + 0.25 * source[i + 1];
            }
            return result;
        }
        /// 
        /// Усечённое среднее (отбрасывает по 15% самых малых и самых больших задержек для защиты от артефактов движения).
        /// 
        private static double CalculateTrimmedMean(List<double> values)
        {
            if (values.Count < 4)
                return values.Average();
            var sorted = values.OrderBy(v => v).ToList();
            int trim = (int)(sorted.Count * 0.15);
            var trimmed = sorted.Skip(trim).Take(sorted.Count - 2 * trim);
            return trimmed.Average();
        }

        private static bool HasSignalActivity(int[] signal, int length)
        {
            int min = signal[0];
            int max = signal[0];

            for (int i = 1; i < length; i++)
            {
                if (signal[i] < min) min = signal[i];
                if (signal[i] > max) max = signal[i];
            }

            // Если размах сигнала меньше 500 единиц АЦП (нет пульсовой волны, только шум) — считаем канал пустым
            return (max - min) > 500;
        }
    }
}
