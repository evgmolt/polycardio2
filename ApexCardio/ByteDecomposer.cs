using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Polycardio2
{
    public class DataArrays
    {
        public int[] ECG1Array;
        public int[] ECG2Array;
        public int[] Reo1Array;
        public int[] Reo2Array;
        public int[] SphigmoLeftHandArray;
        public int[] SphigmoLeftLegArray;
        public int[] SphigmoRightHandArray;
        public int[] SphigmoRightLegArray;
        public int[] ApexArray;
        public int[] PPGArray;
        public short[] SoundArray;

        public int[] ECG1ViewArray;
        public int[] ECG2ViewArray;
        public int[] Reo1ViewArray;
        public int[] Reo2ViewArray;
        public int[] SphigmoLeftHandViewArray;
        public int[] SphigmoLeftLegViewArray;
        public int[] SphigmoRightHandViewArray;
        public int[] SphigmoRightLegViewArray;
        public int[] ApexViewArray;
        public int[] PPGViewArray;
        public int[] SoundViewArray;

        public List<int[]> ViewList;
        
        private const int RRMaxNum = 1000;
        public int[] RArray;
        public int RInd;
        public int FragSize;
        public byte NIBP1_Syst;
        public byte NIBP1_Diast;
        public ushort NIBP1_Press;
        public byte NIBP2_Syst;
        public byte NIBP2_Diast;
        public ushort NIBP2_Press;
        public byte NIBP3_Syst;
        public byte NIBP3_Diast;
        public ushort NIBP3_Press;
        public byte NIBP4_Syst;
        public byte NIBP4_Diast;
        public ushort NIBP4_Press;
        public int DeviceStatus;
    public DataArrays(int size)
        {
            ECG1Array = new int[size];
            ECG2Array = new int[size];
            Reo1Array = new int[size];
            Reo2Array = new int[size];
            SphigmoLeftHandArray = new int[size];
            SphigmoLeftLegArray = new int[size];
            SphigmoRightHandArray = new int[size];
            SphigmoRightLegArray = new int[size];
            ApexArray = new int[size];
            PPGArray = new int[size];
            SoundArray = new short[size * 8];


            ECG1ViewArray = new int[size];
            ECG2ViewArray = new int[size];
            Reo1ViewArray = new int[size];
            Reo2ViewArray = new int[size];
            SphigmoLeftHandViewArray = new int[size];
            SphigmoLeftLegViewArray = new int[size];
            SphigmoRightHandViewArray = new int[size];
            SphigmoRightLegViewArray = new int[size];
            ApexViewArray = new int[size];
            PPGViewArray = new int[size];
            SoundViewArray = new int[size * 8];

            ViewList = new List<int[]>();
            ViewList.Add(ECG1ViewArray);
            ViewList.Add(ECG2ViewArray);
            ViewList.Add(Reo1ViewArray);
            ViewList.Add(Reo2ViewArray);
            ViewList.Add(SphigmoLeftHandViewArray);
            ViewList.Add(SphigmoLeftLegViewArray);
            ViewList.Add(SphigmoRightHandViewArray);
            ViewList.Add(SphigmoRightLegViewArray);
            ViewList.Add(ApexViewArray);
            ViewList.Add(PPGViewArray);
            ViewList.Add(SoundViewArray);

            RArray = new int[RRMaxNum];
            RInd = 0;
        }
    }

    public class BPEventArgs : EventArgs
    {
        public byte status { get; set; }
    }

    class ByteDecomposer
    {
        public bool Calibrate;
        public event EventHandler DecomposeLineEvent;
        public event EventHandler ConnectionBreakdown;
        public event EventHandler BPcompleted;

        public const int SamplingFrequency = 125;

        public DataArrays Data;
        private const byte Marker1 = 0x19;
        public const int DataArrSize = 0x100000;
        public const int DataArrSizeForView = 4000;

        private int ECG1tmp;
        private int ECG2tmp;
        private int Reo1tmp;
        private int Reo2tmp;
        private int Sphigmo1tmp;
        private int Sphigmo2tmp;
        private int Sphigmo3tmp;
        private int Sphigmo4tmp;
        private int Apextmp;

        private int nextECG1;
        private int nextECG2;
        private int nextReo1;
        private int nextReo2;
        private int nextSphigmo1;
        private int nextSphigmo2;
        private int nextSphigmo3;
        private int nextSphigmo4;
        private int nextApex;

        private double detrendECG1 = 0;
        private double detrendECG2 = 0;
        private double detrendReo1 = 0;
        private double detrendReo2 = 0;
        private double detrendSphigmo1 = 0;
        private double detrendSphigmo2 = 0;
        private double detrendSphigmo3 = 0;
        private double detrendSphigmo4 = 0;
        private double detrendApex = 0;

        private int prevECG1;
        private int prevECG2;
        private int prevReo1;
        private int prevReo2;
        private int prevSphigmo1;
        private int prevSphigmo2;
        private int prevSphigmo3;
        private int prevSphigmo4;
        private int prevApex;

        public DeviceStatusFlags Status;

        public uint MainIndex = 0;
        public int LineCounter = 0;
        private const byte AccelZero = 128-105;
        private const int MaxDTOCounter = 10;

        public const int BytesInBlock=27;
        private int byteNum;
        public bool RecordStarted;
        public bool DeviceTurnedOn;
        private int DTOCounter;
        public bool BP1measStarted;
        public bool BP2measStarted;
        public bool BP3measStarted;
        public bool BP4measStarted;
        public bool Pump1Started;
        public bool Pump2Started;
        public bool Pump3Started;
        public bool Pump4Started;
        private bool Pump1RealyStarted;
        private bool Pump2RealyStarted;
        private bool Pump3RealyStarted;
        private bool Pump4RealyStarted;

        private int DelayCounter1;
        private int DelayCounter2;
        private const int MaxDelayCounter = 300;
        public int TotalBytes;
        const double filterCoeff = 0.005;
        const double filterCoeffReo = 0.01;


        public ByteDecomposer(DataArrays data)
        {
            Calibrate = false;
            Data = data;
            BP1measStarted = false;
            BP2measStarted = false;
            Pump1Started = false;
            Pump2Started = false;
            Pump3Started = false;
            Pump4Started = false;
            Pump1RealyStarted = false;
            Pump2RealyStarted = false;
            Pump3RealyStarted = false;
            Pump4RealyStarted = false;
            RecordStarted = false;
            DeviceTurnedOn = true;
            DTOCounter = 0;
            TotalBytes = 0;
            byteNum = 0;
            MainIndex = 0;
        }

        protected virtual void OnDecomposeLineEvent()
        {
            if (DecomposeLineEvent != null)
                DecomposeLineEvent(this, null);
        }

        protected virtual void OnConnectionBreakdown()
        {
            if (ConnectionBreakdown != null)
                ConnectionBreakdown(this, null);
        }

        protected virtual void OnBPcompleted(EventArgs e)
        {
            if (BPcompleted != null)
                BPcompleted(this, e);
        }

        public int Decompos(USBserialPort usbport)
        {
            return Decompos(usbport, null, null, null);
        }

        private double FilterRecursDetrend(double filterK, double next, double detrend, double prev)
        {
            return ((1 - filterK) * next - (1 - filterK) * prev + (1 - 2 * filterK) * detrend);
        }

        private void ProcessData(int[] inData, int[] outData, int size, bool fOn, double[] coeff)
        {
            int Aver = 0;
            var tmpBuf = new int[size];
            for (int i = 0; i < size; i++)
            {
                Aver = Aver + inData[i];
            }
            Aver = Aver / size;
            for (int i = 0; i < size; i++)
            {
                tmpBuf[i] = inData[i] - Aver;
            }
            for (int i = 0; i < size; i++)
            {
                if (fOn)
                    outData[i] = Filter.filterForView(coeff, tmpBuf, i);
                else outData[i] = tmpBuf[i];
            }
        }


        public void CountViewArrays(int size, bool f)
        {
            ProcessData(Data.ECG1Array, Data.ECG1ViewArray , size, f, Filter.coeff50);
            ProcessData(Data.ECG2Array, Data.ECG2ViewArray, size, f, Filter.coeff50);
            ProcessData(Data.Reo1Array, Data.Reo1ViewArray, size, f, Filter.coeff14);
            ProcessData(Data.Reo2Array, Data.Reo2ViewArray, size, f, Filter.coeff14);
            ProcessData(Data.SphigmoLeftHandArray, Data.SphigmoLeftHandViewArray, size, f, Filter.coeff14);
            ProcessData(Data.SphigmoLeftLegArray, Data.SphigmoLeftLegViewArray, size, f, Filter.coeff14);
            ProcessData(Data.ApexArray, Data.ApexViewArray, size, f, Filter.coeff14);
        }

        private void ApplyDSPFilters(ApexConfig cfg)
        {
            bool filterOn = cfg != null && cfg.FilterOn;

            // --- ЭКГ 1 (L-R) ---
            nextECG1 = Data.ECG1Array[MainIndex];
            if (filterOn)
            {
                nextECG1 = Filter.filter(Filter.coeff50, Data.ECG1Array, MainIndex);
            }
            detrendECG1 = FilterRecursDetrend(filterCoeff, nextECG1, detrendECG1, prevECG1);
            Data.ECG1ViewArray[MainIndex] = (int)Math.Round(detrendECG1);
            prevECG1 = nextECG1;

            // --- ЭКГ 2 (F-R) ---
            nextECG2 = Data.ECG2Array[MainIndex];
            if (filterOn)
            {
                nextECG2 = Filter.filter(Filter.coeff50, Data.ECG2Array, MainIndex);
            }
            detrendECG2 = FilterRecursDetrend(filterCoeff, nextECG2, detrendECG2, prevECG2);
            Data.ECG2ViewArray[MainIndex] = (int)Math.Round(detrendECG2);
            prevECG2 = nextECG2;

            // --- Реограмма 1 ---
            // Для фильтра передаем модуль значения (как в исходной логике)
            nextReo1 = Math.Abs(Data.Reo1Array[MainIndex]);
            if (filterOn)
            {
                nextReo1 = Filter.filter(Filter.coeff14, Data.Reo1Array, MainIndex);
            }
            detrendReo1 = FilterRecursDetrend(filterCoeffReo, nextReo1, detrendReo1, prevReo1);
            Data.Reo1ViewArray[MainIndex] = (int)Math.Round(detrendReo1);
            prevReo1 = nextReo1;

            // --- Реограмма 2 ---
            nextReo2 = Math.Abs(Data.Reo2Array[MainIndex]);
            if (filterOn)
            {
                nextReo2 = Filter.filter(Filter.coeff14, Data.Reo2Array, MainIndex);
            }
            detrendReo2 = FilterRecursDetrend(filterCoeffReo, nextReo2, detrendReo2, prevReo2);
            // Пишется со знаком минус на отображение
            Data.Reo2ViewArray[MainIndex] = -(int)Math.Round(detrendReo2);
            prevReo2 = nextReo2;

            // --- Сфигмограмма 1 (Левая Рука) ---
            nextSphigmo1 = Data.SphigmoLeftHandArray[MainIndex];
            if (filterOn)
            {
                nextSphigmo1 = Filter.filter(Filter.coeff14, Data.SphigmoLeftHandArray, MainIndex);
            }
            detrendSphigmo1 = FilterRecursDetrend(filterCoeff, nextSphigmo1, detrendSphigmo1, prevSphigmo1);
            Data.SphigmoLeftHandViewArray[MainIndex] = (int)Math.Round(detrendSphigmo1);
            prevSphigmo1 = nextSphigmo1;

            // --- Сфигмограмма 2 (Левая Нога) ---
            nextSphigmo2 = Data.SphigmoLeftLegArray[MainIndex];
            if (filterOn)
            {
                nextSphigmo2 = Filter.filter(Filter.coeff14, Data.SphigmoLeftLegArray, MainIndex);
            }
            detrendSphigmo2 = FilterRecursDetrend(filterCoeff, nextSphigmo2, detrendSphigmo2, prevSphigmo2);
            Data.SphigmoLeftLegViewArray[MainIndex] = (int)Math.Round(detrendSphigmo2);
            prevSphigmo2 = nextSphigmo2;

            // --- Сфигмограмма 3 (Правая Рука) ---
            // Используем новые переменные делей-фильтра (убедитесь, что они объявлены в полях класса)
            nextSphigmo3 = Data.SphigmoRightHandArray[MainIndex];
            if (filterOn)
            {
                nextSphigmo3 = Filter.filter(Filter.coeff14, Data.SphigmoRightHandArray, MainIndex);
            }
            detrendSphigmo3 = FilterRecursDetrend(filterCoeff, nextSphigmo3, detrendSphigmo3, prevSphigmo3);
            Data.SphigmoRightHandViewArray[MainIndex] = (int)Math.Round(detrendSphigmo3);
            prevSphigmo3 = nextSphigmo3;

            // --- Сфигмограмма 4 (Правая Нога) ---
            nextSphigmo4 = Data.SphigmoRightLegArray[MainIndex];
            if (filterOn)
            {
                nextSphigmo4 = Filter.filter(Filter.coeff14, Data.SphigmoRightLegArray, MainIndex);
            }
            detrendSphigmo4 = FilterRecursDetrend(filterCoeff, nextSphigmo4, detrendSphigmo4, prevSphigmo4);
            Data.SphigmoRightLegViewArray[MainIndex] = (int)Math.Round(detrendSphigmo4);
            prevSphigmo4 = nextSphigmo4;

            // --- Апекскардиограмма ---
            nextApex = Data.ApexArray[MainIndex];
            if (filterOn)
            {
                nextApex = Filter.filter(Filter.coeff14, Data.ApexArray, MainIndex);
            }
            detrendApex = FilterRecursDetrend(filterCoeff, nextApex, detrendApex, prevApex);
            Data.ApexViewArray[MainIndex] = (int)Math.Round(detrendApex);
            prevApex = nextApex;

            // --- Фотоплетизмограмма (PPG) ---
            // Записываем отфильтрованное или сырое значение в отображаемый массив ApexPPGArray
            Data.ApexViewArray[MainIndex] = Data.PPGArray[MainIndex];
        }

        private void ParseFullPacket(ReadOnlySpan<byte> packet, ApexConfig cfg)
        {
            // 1. Чтение 24-битных знаковых данных (ЭКГ, Рео, Сфигмо, Апекс, PPG)
            Data.ECG1Array[MainIndex] = ReadInt24LE(packet.Slice(2, 3));
            Data.ECG2Array[MainIndex] = ReadInt24LE(packet.Slice(5, 3));

            // Реограммы (с ограничением и инверсией)
            int reo1 = ReadInt24LE(packet.Slice(8, 3));
            Data.Reo1Array[MainIndex] = -(reo1 > ApexConstants.ReoMaxVal ? ApexConstants.ReoMaxVal : reo1);

            int reo2 = ReadInt24LE(packet.Slice(11, 3));
            Data.Reo2Array[MainIndex] = -(reo2 > ApexConstants.ReoMaxVal ? ApexConstants.ReoMaxVal : reo2);

            // Сфигмограммы и Апекс
            Data.SphigmoLeftHandArray[MainIndex] = ReadInt24LE(packet.Slice(14, 3));
            Data.SphigmoLeftLegArray[MainIndex] = ReadInt24LE(packet.Slice(17, 3));
            Data.SphigmoRightHandArray[MainIndex] = ReadInt24LE(packet.Slice(20, 3));
            Data.SphigmoRightLegArray[MainIndex] = ReadInt24LE(packet.Slice(23, 3));
            Data.ApexArray[MainIndex] = ReadInt24LE(packet.Slice(26, 3));
            Data.PPGArray[MainIndex] = ReadInt24LE(packet.Slice(29, 3));

            // Накатываем фильтры и детрендинг для отображения (View)
            ApplyDSPFilters(cfg);

            // 2. Чтение 16-битных данных звука микрофона (8 отсчетов по 2 байта)
            uint soundIdx = MainIndex << 3;
            int soundOffset = 32;
            for (int j = 0; j < 8; j++)
            {
                Data.SoundArray[soundIdx + j] = System.Buffers.Binary.BinaryPrimitives.ReadInt16LittleEndian(packet.Slice(soundOffset, 2));
                soundOffset += 2;
            }

            // 3. Чтение блоков давления и статуса (только если пакет длинный — 67 байт)
            if (packet.Length == 67)
            {
                Data.NIBP1_Syst = packet[48]; Data.NIBP1_Diast = packet[49];
                Data.NIBP1_Press = System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(packet.Slice(50, 2));

                Data.NIBP2_Syst = packet[52]; Data.NIBP2_Diast = packet[53];
                Data.NIBP2_Press = System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(packet.Slice(54, 2));

                Data.NIBP3_Syst = packet[56]; Data.NIBP3_Diast = packet[57];
                Data.NIBP3_Press = System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(packet.Slice(58, 2));

                Data.NIBP4_Syst = packet[60]; Data.NIBP4_Diast = packet[61];
                Data.NIBP4_Press = System.Buffers.Binary.BinaryPrimitives.ReadUInt16LittleEndian(packet.Slice(62, 2));

                // 3-байтовый статус
                Data.DeviceStatus = packet[64] | (packet[65] << 8) | (packet[66] << 16);
                Status = (DeviceStatusFlags) Data.DeviceStatus;
                ProcessDeviceStatus(); // Выносим логику насосов и OnBPcompleted сюда
            }
        }

        private void ProcessDeviceStatus()
        {

            // --- Интеграция с вашей старой логикой компрессоров (по битовым маскам) ---
            BP1measStarted = Status.HasFlag(DeviceStatusFlags.dsBP1_InProgress);
            BP2measStarted = Status.HasFlag(DeviceStatusFlags.dsBP2_InProgress);
            BP3measStarted = Status.HasFlag(DeviceStatusFlags.dsBP3_InProgress);
            BP4measStarted = Status.HasFlag(DeviceStatusFlags.dsBP4_InProgress);
            OnBPcompleted(EventArgs.Empty);
/*
            // Модуль давления 1 (Левая рука)
            if (Pump1Started && (Data.DeviceStatus & pump1progress) != 0) Pump1RealyStarted = true;
            if (Pump1RealyStarted) Data.SphigmoLeftHandViewArray[MainIndex] = 0;
            if (Pump1RealyStarted && (Data.DeviceStatus & pump1progress) == 0)
            {
                if (++DelayCounter1 > MaxDelayCounter)
                {
                    Pump1Started = false; Pump1RealyStarted = false; detrendSphigmo1 = 0; DelayCounter1 = 0;
                }
            }

            // Модуль давления 2 (Левая нога)
            if (Pump2Started && (Data.DeviceStatus & pump2progress) != 0) Pump2RealyStarted = true;
            if (Pump2RealyStarted)
            {
                detrendSphigmo2 = 0;
                Data.SphigmoLeftLegViewArray[MainIndex] = (int)Math.Round(detrendSphigmo2);
            }
            if (Pump2RealyStarted && (Data.DeviceStatus & pump2progress) == 0)
            {
                if (++DelayCounter2 > MaxDelayCounter)
                {
                    Pump2Started = false; Pump2RealyStarted = false; detrendSphigmo2 = 0; DelayCounter2 = 0;
                }
            }
*/

        }


        // Удобный хелпер для чтения 3-байтовых знаковых чисел (Int24 Little Endian)
        private int ReadInt24LE(ReadOnlySpan<byte> slice)
        {
            int val = slice[0] | (slice[1] << 8) | (slice[2] << 16);
            return (val & 0x800000) != 0 ? val - 0x1000000 : val;
        }

        public int Decompos(USBserialPort usbport, Stream saveFileStream)
        {
            return Decompos(usbport, saveFileStream, null, null);
        }
        private readonly byte[] _packetBuffer = new byte[67]; // Буфер под максимальный размер пакета
        private int _packetIdx = 0;                          // Текущая позиция заполнения буфера
        private int _currentPacketSize = 48;                 // Ожидаемый размер (динамически меняется)


        public int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream, ApexConfig cfg)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                if (++DTOCounter > MaxDTOCounter) DeviceTurnedOn = false;
                return 0;
            }

            DeviceTurnedOn = true;

            if (saveFileStream != null && RecordStarted)
            {
                try
                {
                    saveFileStream.Write(usbport.PortBuf, 0, bytes);
                    TotalBytes += bytes;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }

            for (int i = 0; i < bytes; i++)
            {
                byte currentByte = usbport.PortBuf[i];

                // ШАГ 1: Поиск маркера начала посылки (Offset 0)
                if (_packetIdx == 0)
                {
                    if (currentByte != 0x19) continue; // Пропускаем байты до маркера
                    _packetBuffer[_packetIdx++] = currentByte;
                    continue;
                }

                // ШАГ 2: Чтение размера пакета (Offset 1)
                if (_packetIdx == 1)
                {
                    if (currentByte == 48 || currentByte == 67)
                    {
                        _currentPacketSize = currentByte;
                        _packetBuffer[_packetIdx++] = currentByte;
                    }
                    else
                    {
                        _packetIdx = 0; // Сброс: пришел некорректный размер пакета
                    }
                    continue;
                }

                // ШАГ 3: Накопление тела пакета
                _packetBuffer[_packetIdx++] = currentByte;

                // ШАГ 4: Пакет собран полностью — запускаем парсинг и финализацию
                if (_packetIdx >= _currentPacketSize)
                {
                    ParseFullPacket(_packetBuffer.AsSpan(0, _currentPacketSize), cfg);

                    // Запись в файл, вызов событий и сдвиг кольцевого MainIndex
                    FinalizePacket(txtFileStream);

                    _packetIdx = 0; // Сброс буфера для следующего пакета
                }
            }

            usbport.BytesRead = 0;
            return bytes;
        }

        private void FinalizePacket(StreamWriter? txtFileStream)
        {
            // Запись в текстовый лог, если запущена запись
            if (RecordStarted && txtFileStream != null)
            {
                // Передаем MainIndex (кастуем к int, если метод GetDataString принимает int)
                txtFileStream.WriteLine(GetDataString(MainIndex));
            }

            // Вызов события отрисовки интерфейса
            OnDecomposeLineEvent();

            // Инкрементируем счетчик линий
            LineCounter++;

            // Сдвигаем кольцевой индекс и сбрасываем по маске степени двойки
            MainIndex++;
            MainIndex &= (uint)(DataArrSize - 1);
        }

        private string GetDataString(uint index)
        {
            // Рассчитываем стартовый индекс в массиве звука для текущего кадра (index * 8)
            uint soundIdx = index << 3;

            // Высокопроизводительная интерполяция строк .NET 8/9 (автоматически компилируется в оптимальный StringHandler)
            return $"{DateTime.Now}\t" +
                   $"{Data.ECG1Array[index]}\t" +
                   $"{Data.ECG2Array[index]}\t" +
                   $"{Data.Reo1Array[index]}\t" +
                   $"{Data.Reo2Array[index]}\t" +
                   $"{Data.SphigmoLeftHandArray[index]}\t" +
                   $"{Data.SphigmoLeftLegArray[index]}\t" +
                   $"{Data.SphigmoRightHandArray[index]}\t" + // Новый канал: Sfig3
                   $"{Data.SphigmoRightLegArray[index]}\t" +  // Новый канал: Sfig4
                   $"{Data.ApexArray[index]}\t" +
                   $"{Data.PPGArray[index]}\t" +               // Новый канал: Фотоплетизмограмма

                   // Выгрузка 8 последовательных отсчетов микрофона (ФКГ)
                   $"{Data.SoundArray[soundIdx]}\t" +
                   $"{Data.SoundArray[soundIdx + 1]}\t" +
                   $"{Data.SoundArray[soundIdx + 2]}\t" +
                   $"{Data.SoundArray[soundIdx + 3]}\t" +
                   $"{Data.SoundArray[soundIdx + 4]}\t" +
                   $"{Data.SoundArray[soundIdx + 5]}\t" +
                   $"{Data.SoundArray[soundIdx + 6]}\t" +
                   $"{Data.SoundArray[soundIdx + 7]}\t" +

                   // Выгрузка данных артериального давления NIBP 1-4 (SYS, DIA, текущее Press)
                   $"{Data.NIBP1_Syst}\t{Data.NIBP1_Diast}\t{Data.NIBP1_Press}\t" +
                   $"{Data.NIBP2_Syst}\t{Data.NIBP2_Diast}\t{Data.NIBP2_Press}\t" +
                   $"{Data.NIBP3_Syst}\t{Data.NIBP3_Diast}\t{Data.NIBP3_Press}\t" +
                   $"{Data.NIBP4_Syst}\t{Data.NIBP4_Diast}\t{Data.NIBP4_Press}\t" +

                   // Текущее полное состояние прибора (3-байтовое)
                   $"{Data.DeviceStatus}";
        }

    }
}
