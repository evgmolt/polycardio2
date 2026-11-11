using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycardio2
{
    public static class DataProcessing
    {
        public const int SamplingFrequency = 125;
        public static ByteDecomposer ParseData(string[] lines, DataArrays a, int skip)
        {
            for (int i = skip; i < lines.Length; i++)
            {
                string[] s = lines[i].Split(Convert.ToChar(9));
                a.ECG1Array[i - skip] = Convert.ToInt32(s[1]);
                a.ECG2Array[i - skip] = Convert.ToInt32(s[2]);
                a.Reo1Array[i - skip] = Convert.ToInt32(s[3]);
                a.Reo2Array[i - skip] = Convert.ToInt32(s[4]);
                a.SphigmoLeftHandArray[i - skip] = Convert.ToInt32(s[5]);
                a.SphigmoLeftLegArray[i - skip] = Convert.ToInt32(s[6]);
                a.SphigmoRightHandArray[i - skip] = Convert.ToInt32(s[7]);
                a.SphigmoRightLegArray[i - skip] = Convert.ToInt32(s[8]);
                a.ApexArray[i - skip] = Convert.ToInt32(s[9]);
                a.PPGArray[i - skip] = Convert.ToInt32(s[10]);
                a.SoundArray[i - skip] = Convert.ToInt16(s[11]);
            }
            var d = new ByteDecomposer(a);
            d.MainIndex = (uint)lines.Length;
            return d;
        }
        public static void DetectECG(DataArrays D, uint index)
        {
            D.RInd = 0;
            int InsideC = 0;
            int start = 0;
            uint stop = index - 100;
            int max = 0;

            for (int i = start; i < stop; i++)
            {
                max = Math.Max(max, (int)Math.Round(DataProcessing.GetCorr(D.ECG1ViewArray, i)));
            }

            double DetectLevel = max / 2.0;
            bool Rnow = false;
            InsideC = 0;

            for (int i = start; i < stop; i++)
            {
                InsideC++;
                if (InsideC > 20)
                {
                    if (!Rnow)
                    {
                        if (DataProcessing.GetCorr(D.ECG1ViewArray, i) > DetectLevel)
                        {
                            Rnow = true;
                        }
                    }
                    else
                    {
                        if (D.ECG1ViewArray[i] < D.ECG1ViewArray[i - 1])
                        {
                            D.RArray[D.RInd] = i + DataProcessing.CorrPattLen / 2;
                            D.RInd++;
                            InsideC = 0;
                            Rnow = false;
                        }
                    }
                }
            }

            for (int i = 0; i < D.RInd; i++)
            {
                SearchP(D, i);
            }
        }

        private static void SearchP(DataArrays D, int ind)
        {
            const int PRInterval = 20;
            int RPos = D.RArray[ind];
            int pos = RPos - 3;

            // ЗАЩИТА: Добавлено pos > 0, чтобы избежать падения программы
            while (pos > 0 && D.ECG1ViewArray[pos] < D.ECG1ViewArray[pos + 1])
            {
                pos--;
            }

            // ИСПРАВЛЕНО: Инициализируем первым значением, а не нулем (защита от отрицательного дрейфа изолинии)
            int Max = D.ECG1ViewArray[pos];
            int MaxPos = pos;

            // ЗАЩИТА: Ограничиваем левую границу, чтобы индекс не ушел ниже 0
            int leftLimit = Math.Max(0, RPos - PRInterval);

            for (int i = pos; i > leftLimit; i--)
            {
                if (D.ECG1ViewArray[i] > Max)
                {
                    Max = D.ECG1ViewArray[i];
                    MaxPos = i;
                }
            }

            // Перезаписываем временный R-зубец на итоговый P-зубец
            D.RArray[ind] = MaxPos;
        }

        /// <summary>
        /// Надежный поиск систолических максимумов на реальном живом сигнале PPG (ФПГ).
        /// </summary>
        /// <param name="ppgArray">Массив данных PPG (желательно после удаления дрейфа ViewArray)</param>
        /// <param name="dataSize">Фактическая длина заполненных данных в массиве</param>
        /// <param name="samplingRate">Частота дискретизации прибора (для PolyCardio2 это 125 Гц)</param>
        /// <returns>Список индексов истинных пульсовых пиков</returns>
        public static List<int> FindRealPPGMaximums(int[] ppgArray, uint dataSize)
    {
        List<int> peakIndices = new List<int>();

        if (ppgArray == null || dataSize < 25) return peakIndices;

        // 1. Физиологические константы:
        // Рефрактерный период (время абсолютной невозбудимости сердца). 
        // Между ударами пульса должно пройти минимум ~280 мс (максимальный ЧСС ~210 уд/мин).
        // Для частоты 125 Гц это: 0.280 * 125 = 35 отсчетов.
        int minSamplesBetweenPeaks = (int)Math.Round(0.280 * SamplingFrequency);

        // Размер окна для расчета скользящего среднего (динамического порога).
        // Окно должно быть примерно равным длительности одного сердечного цикла (~0.8-1.0 секунды).
        // Для 125 Гц берем окно в 125 отсчетов.
        int windowSize = (int)Math.Round(1.0 * SamplingFrequency);

        // 2. Предварительный расчет динамического порога (Скользящее среднее + поправка)
        double[] adaptiveThreshold = new double[dataSize];
        double runningSum = 0;

        // Инициализируем первое окно
        for (int i = 0; i < Math.Min(windowSize, dataSize); i++)
        {
            runningSum += ppgArray[i];
        }

        for (int i = 0; i < dataSize; i++)
        {
            // Сдвигаем окно скользящего среднего
            if (i >= windowSize)
            {
                runningSum += ppgArray[i] - ppgArray[i - windowSize];
            }

            double mean = runningSum / Math.Min(windowSize, i + 1);

            // Динамический порог — это среднее значение сигнала + 15% от стандартного размаха,
            // чтобы отсечь дикротическую вырезку и дыхательные волны.
            adaptiveThreshold[i] = mean + 400.0; // Для реального сигнала можно настроить коэффициент
        }

        // 3. Основной цикл поиска пиков
        int lastPeakIndex = -minSamplesBetweenPeaks; // Инициализируем далеко в прошлом

        for (int i = 2; i < dataSize - 2; i++)
        {
            int curr = ppgArray[i];

            // Проверяем, что точка выше скользящего динамического порога
            if (curr > adaptiveThreshold[i])
            {
                // Проверяем локальный пик (сглаженный по 5 точкам, чтобы убрать ВЧ-шум)
                if (curr >= ppgArray[i - 1] && curr >= ppgArray[i - 2] &&
                    curr > ppgArray[i + 1] && curr > ppgArray[i + 2])
                {
                    // Защита по времени: проверяем, прошло ли достаточно времени с прошлого удара
                    if (i - lastPeakIndex >= minSamplesBetweenPeaks)
                    {
                        peakIndices.Add(i);
                        lastPeakIndex = i; // Запоминаем индекс последнего подтвержденного пика
                    }
                    else
                    {
                        // Если новый пик оказался выше предыдущего внутри рефрактерного периода,
                        // значит прошлый выбор был ошибочным (попали на шум), обновляем вершину.
                        if (peakIndices.Count > 0 && curr > ppgArray[peakIndices[peakIndices.Count - 1]])
                        {
                            peakIndices[peakIndices.Count - 1] = i;
                            lastPeakIndex = i;
                        }
                    }
                }
            }
        }

        return peakIndices;
    }

    public static int CorrPattLen = 14;
        public static double GetCorr(int[] inArr, int ind)
        {
            int[] CorrPattern = {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
            double patmean = 0;
            for (int i = 0; i < CorrPattern.Length; i++)
            {
                patmean = patmean + inArr[ind + i];
            }
            double smean = 0;
            for (int i = 0; i < CorrPattern.Length; i++)
            {
                smean = smean + inArr[ind + i];
            }
            smean = smean / CorrPattern.Length;
            double corr = 0;
            for (int i = 0; i < CorrPattern.Length; i++)
            {
                corr = corr + (inArr[ind + i] - smean) * (CorrPattern[i] - patmean);
            }
            return corr;
        }

        public static void CountAver(int[] aver, int[] data, int[] rrarray, int numofqrs, int shift)
        {
            if (numofqrs < 1) return;
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = 0;
            }
            int AverArrayCenter = aver.Length / 2;
            int AverSize = rrarray[1] - rrarray[0];
            AverSize = AverSize * 2;
            for (int i = 0; i < numofqrs; i++)
            {
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter - j] += data[rrarray[i + shift] - j];
                }
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter + 1 + j] += data[rrarray[i + shift] + 1 + j];
                }
            }
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = aver[i] / numofqrs;
            }
        }

        public static void CountAver(double[] aver, double[] data, int[] rrarray, int numofqrs, int shift)
        {
            if (numofqrs < 1) return;
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = 0;
            }
            int AverArrayCenter = aver.Length / 2;
            int AverSize = rrarray[1] - rrarray[0];
            AverSize = AverSize * 2;
            for (int i = 0; i < numofqrs; i++)
            {
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter - j] += data[rrarray[i + shift] - j];
                }
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter + 1 + j] += data[rrarray[i + shift] + 1 + j];
                }
            }
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = aver[i] / numofqrs;
            }
        }

        public static double[] Norm(double[] data)
        {
            if (data == null) return null;
            double[] res = new double[data.Length];
            double max = -100000;
            double min = 100000;
            for (int i = 0; i < data.Length; i++)
            {
                max = Math.Max(data[i], max);
                min = Math.Max(data[i], min);
            }
            for (int i = 0; i < data.Length; i++)
            {
                res[i] = (data[i] - min) / (max - min);
            }
            return res;
        }

        public static List<int> FindPoints(double[] data, int size, int shift)
        {
            int T0_T1 = 12;
            List<int> res = new List<int>();
            int T0 = shift;
            res.Add(T0);
            int T1 = shift + T0_T1;
            res.Add(T1);
            if (data[shift+T0_T1] < 0) return null;
            int ind = T1;
            while (data[ind] > 0)
            {
                ind++;
            }
            double max = 0;
            int maxind = 0;
            int T3 = ind;
            for (int i = T1; i < T3; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    maxind = i;
                }
            }
            int T2 = maxind;
            res.Add(T2);
            res.Add(T3);
            ind = T3;
            while (data[ind] < 0)
            {
                ind++;
            }
            double min = 0;
            int minind = 0;
            for (int i = T3; i < ind; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                    minind = i;
                }
            }
            int T4 = minind;
            res.Add(T4);
            ind = T4;
            while (data[ind] < 0)
            {
                ind++;
            }
            int zeroT4T5 = ind;
            while (data[ind] > 0)
            {
                ind++;
            }
            max = 0;
            maxind = 0;
            for (int i = zeroT4T5; i < ind; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    maxind = i;
                }
            }
            int T5 = maxind;
            res.Add(T5);
            while (data[ind] > 0)
            {
                ind++;
            }
            int zeroT5T6 = ind;
            while (data[ind] < 0)
            {
                ind++;
            }
            min = 0;
            minind = 0;
            for (int i = zeroT5T6; i < ind; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                    minind = i;
                }
            }
            int T6 = minind;
            res.Add(T6);
            while (data[ind] < 0)
            {
                ind++;
            }
            int zeroT6T7 = ind;
            while (data[ind] > 0)
            {
                ind++;
            }
            max = 0;
            maxind = 0;
            for (int i = zeroT6T7; i < ind; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    maxind = i;
                }
            }
            int T7 = maxind;
            res.Add(T7);

            min = 0;
            minind = 0;
            for (int i = T7; i < size - shift; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                    minind = i;
                }
            }
            int T8 = minind;
            res.Add(T8);

            res.Add(size - shift);
            return res;
        }

        public static void CountAver(double[] aver, int[] data, int[] rrarray, int numofqrs, int shift)
        {
            if (numofqrs < 1) return;
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = 0;
            }
            int AverArrayCenter = aver.Length / 2;
            int AverSize = rrarray[1] - rrarray[0];
            AverSize = AverSize * 2;
            for (int i = 0; i < numofqrs; i++)
            {
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter - j] += data[rrarray[i + shift] - j];
                }
                for (int j = 0; j < AverSize / 2; j++)
                {
                    aver[AverArrayCenter + 1 + j] += data[rrarray[i + shift] + 1 + j];
                }
            }
            for (int i = 0; i < aver.Length; i++)
            {
                aver[i] = aver[i] / numofqrs;
            }
        }

        public static int GetRange(int[] Data, int size)
        {
            int min = 10000000;
            int max = 0;
            foreach (int elem in Data)
            {
                min = Math.Min(min, elem);
                max = Math.Max(max, elem);
            }
            return (max - min);
        }

        public static void CountDerivative(double[] inarr, double[] outarr, int size)
        {
            for (int i = 2; i < size - 2; i++)
            {
                outarr[i+2] = -2 * inarr[i - 2] - inarr[i - 1] + inarr[i + 1] + 2 * inarr[i + 2];
            }

        }

        public static void CutArray(int[] data, int cutsize)
        {
            if (data == null) return;
            for (int i = 0; i < data.Length / 2; i++)
            {
                if (data[i] != 0)
                {
                    for (int j = 0; j < cutsize; j++)
                    {
                        data[i + j] = 0;
                    }
                    break;
                }
            }
            for (int i = data.Length - 1; i > data.Length / 2; i--)
            {
                if (data[i] != 0)
                {
                    for (int j = 0; j < cutsize; j++)
                    {
                        data[i - j] = 0;
                    }
                    break;
                }
            }
        }

        public static void CutArray(double[] data, int cutsize)
        {
            if (data == null) return;
            for (int i = 0; i < data.Length / 2; i++)
            {
                if (data[i] != 0)
                {
                    for (int j = 0; j < cutsize; j++)
                    {
                        data[i + j] = 0;
                    }
                    break;
                }
            }
            for (int i = data.Length - 1; i > data.Length / 2; i--)
            {
                if (data[i] != 0)
                {
                    for (int j = 0; j < cutsize; j++)
                    {
                        data[i - j] = 0;
                    }
                    break;
                }
            }
        }
    }
}
