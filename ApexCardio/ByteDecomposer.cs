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
        public int status { get; set; }
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
        private int Ppgtmp;
        private short mphTmp;

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
        private int currentPacketSize;

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



        // Удобный хелпер для чтения 3-байтовых знаковых чисел (Int24 Little Endian)
        private int ReadInt24LE(ReadOnlySpan<byte> slice)
        {
            int val = slice[0] | (slice[1] << 8) | (slice[2] << 16);
            return (val & 0x800000) != 0 ? val - 0x1000000 : val;
        }

        private readonly byte[] _packetBuffer = new byte[67]; // Буфер под максимальный размер пакета
        private int _packetIdx = 0;                          // Текущая позиция заполнения буфера
        private int _currentPacketSize = 48;                 // Ожидаемый размер (динамически меняется)


        public int Decompos(USBserialPort usbport, Stream saveFileStream, StreamWriter txtFileStream, ApexConfig cfg)
        {
            int bytes = usbport.BytesRead;
            if (bytes == 0)
            {
                DTOCounter++;
                if (DTOCounter > MaxDTOCounter) DeviceTurnedOn = false;
                return 0;
            }
            DeviceTurnedOn = true;

            if (saveFileStream != null && RecordStarted)
            {
                try
                {
                    saveFileStream.Write(usbport.PortBuf, 0, bytes);
                    TotalBytes = TotalBytes + bytes;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            for (int i = 0; i < bytes; i++)
            {
                byte currentByte = usbport.PortBuf[i];

                switch (byteNum)
                {
                    case 0: // cStreamMark (Offset 0)
                        if (currentByte == 0x19) byteNum = 1;
                        break;

                    case 1: // DataSize (Offset 1)
                        if (currentByte == 48 || currentByte == 67)
                        {
                            currentPacketSize = currentByte; // Переменная класса для размера
                            byteNum = 2;
                        }
                        else
                        {
                            byteNum = 0;
                        }
                        break;

                    // --- ECG1_Sample (Offsets 2, 3, 4) ---
                    case 2: ECG1tmp = currentByte; byteNum = 3; break;
                    case 3: ECG1tmp |= currentByte << 8; byteNum = 4; break;
                    case 4:
                        ECG1tmp |= currentByte << 16;
                        if ((ECG1tmp & 0x800000) != 0) ECG1tmp -= 0x1000000;
                        Data.ECG1Array[MainIndex] = ECG1tmp;
                        nextECG1 = ECG1tmp;
                        if (cfg?.FilterOn == true) nextECG1 = Filter.filter(Filter.coeff50, Data.ECG1Array, MainIndex);
                        detrendECG1 = FilterRecursDetrend(filterCoeff, nextECG1, detrendECG1, prevECG1);
                        Data.ECG1ViewArray[MainIndex] = (int)Math.Round(detrendECG1);
                        prevECG1 = nextECG1;
                        byteNum = 5;
                        break;

                    // --- ECG2_Sample (Offsets 5, 6, 7) ---
                    case 5: ECG2tmp = currentByte; byteNum = 6; break;
                    case 6: ECG2tmp |= currentByte << 8; byteNum = 7; break;
                    case 7:
                        ECG2tmp |= currentByte << 16;
                        if ((ECG2tmp & 0x800000) != 0) ECG2tmp -= 0x1000000;
                        Data.ECG2Array[MainIndex] = ECG2tmp;
                        nextECG2 = ECG2tmp;
                        if (cfg?.FilterOn == true) nextECG2 = Filter.filter(Filter.coeff50, Data.ECG2Array, MainIndex);
                        detrendECG2 = FilterRecursDetrend(filterCoeff, nextECG2, detrendECG2, prevECG2);
                        Data.ECG2ViewArray[MainIndex] = (int)Math.Round(detrendECG2);
                        prevECG2 = nextECG2;
                        byteNum = 8;
                        break;

                    // --- Reo1_Sample (Offsets 8, 9, 10) ---
                    case 8: Reo1tmp = currentByte; byteNum = 9; break;
                    case 9: Reo1tmp |= currentByte << 8; byteNum = 10; break;
                    case 10:
                        Reo1tmp |= currentByte << 16;
                        if ((Reo1tmp & 0x800000) != 0) Reo1tmp -= 0x1000000;
                        if (Reo1tmp > ApexConstants.ReoMaxVal) Reo1tmp = ApexConstants.ReoMaxVal;
                        Data.Reo1Array[MainIndex] = -Reo1tmp;
                        nextReo1 = Reo1tmp;
                        if (cfg?.FilterOn == true) nextReo1 = Filter.filter(Filter.coeff14, Data.Reo1Array, MainIndex);
                        detrendReo1 = FilterRecursDetrend(filterCoeffReo, nextReo1, detrendReo1, prevReo1);
                        Data.Reo1ViewArray[MainIndex] = (int)Math.Round(detrendReo1);
                        prevReo1 = nextReo1;
                        byteNum = 11;
                        break;

                    // --- Reo2_Sample (Offsets 11, 12, 13) ---
                    case 11: Reo2tmp = currentByte; byteNum = 12; break;
                    case 12: Reo2tmp |= currentByte << 8; byteNum = 13; break;
                    case 13:
                        Reo2tmp |= currentByte << 16;
                        if ((Reo2tmp & 0x800000) != 0) Reo2tmp -= 0x1000000;
                        if (Reo2tmp > ApexConstants.ReoMaxVal) Reo2tmp = ApexConstants.ReoMaxVal;
                        Data.Reo2Array[MainIndex] = -Reo2tmp;
                        nextReo2 = Reo2tmp;
                        if (cfg?.FilterOn == true) nextReo2 = Filter.filter(Filter.coeff14, Data.Reo2Array, MainIndex);
                        detrendReo2 = FilterRecursDetrend(filterCoeffReo, nextReo2, detrendReo2, prevReo2);
                        Data.Reo2ViewArray[MainIndex] = -(int)Math.Round(detrendReo2);
                        prevReo2 = nextReo2;
                        byteNum = 14;
                        break;

                    // --- Sfig1_Sample (SphigmoLeftHand, Offsets 14, 15, 16) ---
                    case 14: Sphigmo1tmp = currentByte; byteNum = 15; break;
                    case 15: Sphigmo1tmp |= currentByte << 8; byteNum = 16; break;
                    case 16:
                        Sphigmo1tmp |= currentByte << 16;
                        if ((Sphigmo1tmp & 0x800000) != 0) Sphigmo1tmp -= 0x1000000;
                        Data.SphigmoLeftHandArray[MainIndex] = Sphigmo1tmp;
                        nextSphigmo1 = Sphigmo1tmp;
                        if (cfg?.FilterOn == true) nextSphigmo1 = Filter.filter(Filter.coeff14, Data.SphigmoLeftHandArray, MainIndex);
                        detrendSphigmo1 = FilterRecursDetrend(filterCoeff, nextSphigmo1, detrendSphigmo1, prevSphigmo1);
                        Data.SphigmoLeftHandViewArray[MainIndex] = (int)Math.Round(detrendSphigmo1);
                        prevSphigmo1 = nextSphigmo1;
                        byteNum = 17;
                        break;

                    // --- Sfig2_Sample (SphigmoLeftLeg, Offsets 17, 18, 19) ---
                    case 17: Sphigmo2tmp = currentByte; byteNum = 18; break;
                    case 18: Sphigmo2tmp |= currentByte << 8; byteNum = 19; break;
                    case 19:
                        Sphigmo2tmp |= currentByte << 16;
                        if ((Sphigmo2tmp & 0x800000) != 0) Sphigmo2tmp -= 0x1000000;
                        Data.SphigmoLeftLegArray[MainIndex] = Sphigmo2tmp;
                        nextSphigmo2 = Sphigmo2tmp;
                        if (cfg?.FilterOn == true) nextSphigmo2 = Filter.filter(Filter.coeff14, Data.SphigmoLeftLegArray, MainIndex);
                        detrendSphigmo2 = FilterRecursDetrend(filterCoeff, nextSphigmo2, detrendSphigmo2, prevSphigmo2);
                        Data.SphigmoLeftLegViewArray[MainIndex] = (int)Math.Round(detrendSphigmo2);
                        prevSphigmo2 = nextSphigmo2;
                        byteNum = 20;
                        break;

                    // --- Sfig3_Sample (SphigmoRightHand, Offsets 20, 21, 22) ---
                    case 20: Sphigmo3tmp = currentByte; byteNum = 21; break;
                    case 21: Sphigmo3tmp |= currentByte << 8; byteNum = 22; break;
                    case 22:
                        Sphigmo3tmp |= currentByte << 16;
                        if ((Sphigmo3tmp & 0x800000) != 0) Sphigmo3tmp -= 0x1000000;
                        Data.SphigmoRightHandArray[MainIndex] = Sphigmo3tmp;
                        nextSphigmo3 = Sphigmo3tmp;
                        if (cfg?.FilterOn == true) nextSphigmo3 = Filter.filter(Filter.coeff14, Data.SphigmoRightHandArray, MainIndex);
                        detrendSphigmo3 = FilterRecursDetrend(filterCoeff, nextSphigmo3, detrendSphigmo3, prevSphigmo3);
                        Data.SphigmoRightHandViewArray[MainIndex] = (int)Math.Round(detrendSphigmo3);
                        prevSphigmo3 = nextSphigmo3;
                        byteNum = 23;
                        break;

                    // --- Sfig4_Sample (SphigmoRightLeg, Offsets 23, 24, 25) ---
                    case 23: Sphigmo4tmp = currentByte; byteNum = 24; break;
                    case 24: Sphigmo4tmp |= currentByte << 8; byteNum = 25; break;
                    case 25:
                        Sphigmo4tmp |= currentByte << 16;
                        if ((Sphigmo4tmp & 0x800000) != 0) Sphigmo4tmp -= 0x1000000;
                        Data.SphigmoRightLegArray[MainIndex] = Sphigmo4tmp;
                        nextSphigmo4 = Sphigmo4tmp;
                        if (cfg?.FilterOn == true) nextSphigmo4 = Filter.filter(Filter.coeff14, Data.SphigmoRightLegArray, MainIndex);
                        detrendSphigmo4 = FilterRecursDetrend(filterCoeff, nextSphigmo4, detrendSphigmo4, prevSphigmo4);
                        Data.SphigmoRightLegViewArray[MainIndex] = (int)Math.Round(detrendSphigmo4);
                        prevSphigmo4 = nextSphigmo4;
                        byteNum = 26;
                        break;

                    // --- Apex_Sample (Offsets 26, 27, 28) ---
                    case 26: Apextmp = currentByte; byteNum = 27; break;
                    case 27: Apextmp |= currentByte << 8; byteNum = 28; break;
                    case 28:
                        Apextmp |= currentByte << 16;
                        if ((Apextmp & 0x800000) != 0) Apextmp -= 0x1000000;
                        Data.ApexArray[MainIndex] = Apextmp;
                        nextApex = Apextmp;
                        if (cfg?.FilterOn == true) nextApex = Filter.filter(Filter.coeff14, Data.ApexArray, MainIndex);
                        detrendApex = FilterRecursDetrend(filterCoeff, nextApex, detrendApex, prevApex);
                        Data.ApexViewArray[MainIndex] = (int)Math.Round(detrendApex);
                        prevApex = nextApex;
                        byteNum = 29;
                        break;

                    // --- PPG_Sample (Offsets 29, 30, 31) ---
                    case 29: Ppgtmp = currentByte; byteNum = 30; break;
                    case 30: Ppgtmp |= currentByte << 8; byteNum = 31; break;
                    case 31:
                        Ppgtmp |= currentByte << 16;
                        if ((Ppgtmp & 0x800000) != 0) Ppgtmp -= 0x1000000;
                        Data.PPGArray[MainIndex] = Ppgtmp;
                        Data.PPGViewArray[MainIndex] = Ppgtmp; // Базовое копирование во View
                        byteNum = 32;
                        break;

                    // --- MPh_Sample0..7 (Микрофон / SoundArray, Offsets 32–47) ---
                    case int n when (n >= 32 && n <= 47):
                        int sampleIndex = (byteNum - 32) / 2;
                        if (byteNum % 2 == 0)
                        {
                            mphTmp = currentByte;
                            byteNum++;
                        }
                        else
                        {
                            mphTmp |= (short)(currentByte << 8);
                            int soundIdx = (int)(MainIndex * 8 + sampleIndex);
                            Data.SoundArray[soundIdx] = (short)mphTmp;
                            Data.SoundViewArray[soundIdx] = mphTmp; // Базовое копирование во View
                            byteNum++;
                        }

                        if (byteNum == 48 && currentPacketSize == 48)
                        {
                            FinalizePacket(txtFileStream); // Конец короткого пакета (48 байт)
                        }
                        break;
                    // --- Данные с модулей давления (NIBP1..4, Offsets 48–63) ---
                    case 48: Data.NIBP1_Syst = currentByte; byteNum = 49; break;
                    case 49: Data.NIBP1_Diast = currentByte; byteNum = 50; break;
                    case 50: Data.NIBP1_Press = currentByte; byteNum = 51; break;
                    case 51: Data.NIBP1_Press |= (ushort)(currentByte << 8); byteNum = 52; break;

                    case 52: Data.NIBP2_Syst = currentByte; byteNum = 53; break;
                    case 53: Data.NIBP2_Diast = currentByte; byteNum = 54; break;
                    case 54: Data.NIBP2_Press = currentByte; byteNum = 55; break;
                    case 55: Data.NIBP2_Press |= (ushort)(currentByte << 8); byteNum = 56; break;

                    case 56: Data.NIBP3_Syst = currentByte; byteNum = 57; break;
                    case 57: Data.NIBP3_Diast = currentByte; byteNum = 58; break;
                    case 58: Data.NIBP3_Press = currentByte; byteNum = 59; break;
                    case 59: Data.NIBP3_Press |= (ushort)(currentByte << 8); byteNum = 60; break;

                    case 60: Data.NIBP4_Syst = currentByte; byteNum = 61; break;
                    case 61: Data.NIBP4_Diast = currentByte; byteNum = 62; break;
                    case 62: Data.NIBP4_Press = currentByte; byteNum = 63; break;
                    case 63: Data.NIBP4_Press |= (ushort)(currentByte << 8); byteNum = 64; break;

                    // --- DeviceStatus (Offsets 64–66, 3 байта, мл. байт вперед) ---
                    case 64: Data.DeviceStatus = currentByte; byteNum = 65; break;
                    case 65: Data.DeviceStatus |= currentByte << 8; byteNum = 66; break;
                    case 66:
                        Data.DeviceStatus |= currentByte << 16;

                        uint currentStatus = (uint)Data.DeviceStatus;

                        // Проверка флагов работы модулей давления (Идет измерение АД)
                        BP1measStarted = (currentStatus & (uint)DeviceStatusFlags.dsBP1_InProgress) != 0;
                        BP2measStarted = (currentStatus & (uint)DeviceStatusFlags.dsBP2_InProgress) != 0;
                        BP3measStarted = (currentStatus & (uint)DeviceStatusFlags.dsBP3_InProgress) != 0;
                        BP4measStarted = (currentStatus & (uint)DeviceStatusFlags.dsBP4_InProgress) != 0;

                        // Если насос качает воздух на конечности, зануляем отображение сфигмограммы (шумоподавление)
                        if (BP1measStarted) Data.SphigmoLeftHandViewArray[MainIndex] = 0;
                        if (BP2measStarted) Data.SphigmoLeftLegViewArray[MainIndex] = 0;
                        if (BP3measStarted) Data.SphigmoRightHandViewArray[MainIndex] = 0;
                        if (BP4measStarted) Data.SphigmoRightLegViewArray[MainIndex] = 0;

                        // Управление задержками помп (если необходимо перенести логику DelayCounter для 4 каналов)
                        // [При необходимости добавьте сюда вашу логику для Pump1Started..Pump4Started]

                        // Проверка на завершение измерения или ошибку по любому из каналов
                        if (Calibrate || Data.DeviceStatus > 0)
                        {
                            var args = new BPEventArgs { status = Data.DeviceStatus };
                            OnBPcompleted(args);
                        }

                        FinalizePacket(txtFileStream); // Конец полной посылки (67 байт)
                        break;
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
