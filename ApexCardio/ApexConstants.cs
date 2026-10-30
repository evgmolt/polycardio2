using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polycardio2
{
    static class ApexConstants
    {
        public const int NumOfChannels = 11;
        public const int NumOfAmpBars = 7;
        public const byte M1 = 0xE6;
        public const byte ID = 0x61;
        public const byte cmGetID = 0x5A;
        public const byte cmdStreamOn = 0x51;//	-	Запуск потока передачи данных
        public const byte cmdStreamOff = 0x52;//	-	Остановка потока передачи данных
        public const byte cmdStartBP1 = 0x21;//	-	Запуск измерения АД на левой руке
        public const byte cmdCancelBP1 = 0x31;//	-	Отмена измерения АД на левой руке
        public const byte cmdSetPressSfig1 = 0x81;// Pressure    Задание давления для съема сфигмограммы на левой руке(Pressure - в мм.рт.ст.)
        public const byte cmdStartSfig1 = 0x61;//	-	Запуск съема сфигмограммы на левой руке
        public const byte cmdStopSfig1 = 0x71;//	-	Остановка съема сфигмограммы на левой руке
        public const byte cmdStartBP2 = 0x22;//	-	Запуск измерения АД на левой ноге
        public const byte cmdCancelBP2 = 0x32;//	-	Отмена измерения АД на левой ноге
        public const byte cmdSetPressSfig2 = 0x82;// Pressure    Задание давления для съема сфигмограммы на левой ноге(Pressure - в мм.рт.ст.)
        public const byte cmdStartSfig2 = 0x62;//	-	Запуск съема сфигмограммы на левой ноге
        public const byte cmdStopSfig2 = 0x72;//	-	Остановка съема сфигмограммы на левой ноге
        public const byte cmdStartBP3 = 0x23;//	-	Запуск измерения АД на правой руке
        public const byte cmdCancelBP3 = 0x33;//	-	Отмена измерения АД на правой руке
        public const byte cmdSetPressSfig3 = 0x83;// Pressure    Задание давления для съема сфигмограммы на правой руке(Pressure - в мм.рт.ст.)
        public const byte cmdStartSfig3 = 0x63;//	-	Запуск съема сфигмограммы на правой руке
        public const byte cmdStopSfig3 = 0x73;//	-	Остановка съема сфигмограммы на правой руке
        public const byte cmdStartBP4 = 0x24;//	-	Запуск измерения АД на правой ноге
        public const byte cmdCancelBP4 = 0x34;//	-	Отмена измерения АД на правой ноге
        public const byte cmdSetPressSfig4 = 0x84;// Pressure    Задание давления для съема сфигмограммы на правой ноге(Pressure - в мм.рт.ст.)
        public const byte cmdStartSfig4 = 0x64;//	-	Запуск съема сфигмограммы на правой ноге
        public const byte cmdStopSfig4 = 0x74;//	-	Остановка съема сфигмограммы на правой ноге

        public const int USBTimerInterval = 25;
        public static string TextFileExtension = ".txt";
        public static string RecInfoExtension = ".txt";
        public static string TmpTextFile = "tmp.t";

        public static int PatientFieldsCount = 9;
        public static int HeaderAndVisibleCount = 2;
        public static int HeaderSize = PatientFieldsCount + HeaderAndVisibleCount;
        //        public static double ScaleECG_Y = 1;
//        public static double ScalePPG_Y = 1;

        public const string ConfigFileName = "PCConf.cfg";

        public const int ReoMaxVal = 7000000;

        public const string ECG1name = "ECG 1";
        public const string ECG2name = "ECG 2";
        public const string Reo1name = "Reo 1";
        public const string Reo2name = "Reo 2";
        public const string Sphigmo1name = "Sphigmo 1";
        public const string Sphigmo2name = "Sphigmo 2";
        public const string Apexname = "Apex";

    }
}
