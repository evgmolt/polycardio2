using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Polycardio2
{
    public class RecordInfoData
    {
        public DateTime Date;   //0
        public int Length;  //1
        public string Name;     //2
        public string Sex;      //3
        public int Age;     //4
        public int Height;  //5
        public int Weight;  //6
        public byte[] ABP1;         //7
        public byte[] ABP2;         //8
        public byte[] ABP3;         //9
        public byte[] ABP4;         //10
        public int Pulse;
        public string[] ItemNames;


        public RecordInfoData()
        {
            ItemNames = new string[]
                {
                    "Date : ",
                    "Length : ",
                    "Sex : ",
                    "Age : ",
                    "Height : ",
                    "Weight : ",
                    "ABP1 : ",
                    "ABP2 : ",
                    "ABP3 : ",
                    "ABP4 : ",
                    "Pulse : "
                };
            ABP1 = new byte[2];
            ABP2 = new byte[2];
            ABP3 = new byte[2];
            ABP4 = new byte[2];
        }
    }

    public class RecordInfo
    {
        public int NumOfProperties = ApexConstants.HeaderSize;
        private string SignSelected = "V";
        private char ABPseparator = '/';
        public RecordInfoData Data;
        public string FileName;
        private string[] ColumnNames = { "Date/time", "ECG1", "ECG2", "Reo 1", "Reo 2", "Sphigmo 1", "Sphigmo 2", "Sphigmo 3", "Sphigmo 4", "PPG", "Apex", "Sound" };


        public RecordInfo(ApexConfig cfg, string fNameArg, string[] lines)
        {
            FileName = cfg.DataDir + fNameArg;
            Data = new RecordInfoData();
            lines = lines.Select(RemoveItemName).ToArray();
            Data.Date = Convert.ToDateTime(lines[0]);
            Data.Length = Convert.ToInt16(lines[1]);
            Data.Sex = lines[2];
            Data.Age = Convert.ToInt16(lines[3]);
            Data.Height = Convert.ToInt16(lines[4]);
            Data.Weight = Convert.ToInt16(lines[5]);
            string[] s = lines[6].Split(ABPseparator);
            Data.ABP1[0] = Convert.ToByte(s[0]);
            Data.ABP1[1] = Convert.ToByte(s[1]);
            s = lines[7].Split(ABPseparator);
            Data.ABP2[0] = Convert.ToByte(s[0]);
            Data.ABP2[1] = Convert.ToByte(s[1]);
            s = lines[8].Split(ABPseparator);
            Data.ABP3[0] = Convert.ToByte(s[0]);
            Data.ABP3[1] = Convert.ToByte(s[1]);
            s = lines[9].Split(ABPseparator);
            Data.ABP4[0] = Convert.ToByte(s[0]);
            Data.ABP4[1] = Convert.ToByte(s[1]);
            Data.Pulse = Convert.ToByte(lines[10]);
        }


        public RecordInfo(ApexConfig cfg, string fNameArg)
        {
            FileName = cfg.DataDir + fNameArg;
            Data=new RecordInfoData();
            if (File.Exists(FileName))
            try
            {
                var lines = System.IO.File.ReadAllLines(FileName, Encoding.Default);
                lines = lines.Select(RemoveItemName).ToArray();
                Data.Date = Convert.ToDateTime(lines[0]);
                Data.Length = Convert.ToInt16(lines[1]);
                Data.Sex = lines[2];
                Data.Age = Convert.ToInt16(lines[3]);
                Data.Height = Convert.ToInt16(lines[4]);
                Data.Weight = Convert.ToInt16(lines[5]);
                string[] s = lines[6].Split(ABPseparator);
                Data.ABP1[0] = Convert.ToByte(s[0]);
                Data.ABP1[1] = Convert.ToByte(s[1]);
                s = lines[7].Split(ABPseparator);
                Data.ABP2[0] = Convert.ToByte(s[0]);
                Data.ABP2[1] = Convert.ToByte(s[1]);
                s = lines[8].Split(ABPseparator);
                Data.ABP3[0] = Convert.ToByte(s[0]);
                Data.ABP3[1] = Convert.ToByte(s[1]);
                s = lines[9].Split(ABPseparator);
                Data.ABP4[0] = Convert.ToByte(s[0]);
                Data.ABP4[1] = Convert.ToByte(s[1]);
                Data.Pulse = Convert.ToInt16(lines[10]);
                    
            }
                catch (Exception)
            {
            }
        }

        
        public string[] GetRecInfo(bool insertTabs)
        {
            var lines = new string[NumOfProperties - 1];
            lines[0] = AddItemName(insertTabs, 0, Data.Date.ToString());
            lines[1] = AddItemName(insertTabs, 1, Data.Length.ToString());
            lines[2] = AddItemName(insertTabs, 2, Data.Sex);
            lines[3] = AddItemName(insertTabs, 3, Data.Age.ToString());
            lines[4] = AddItemName(insertTabs, 4, Data.Height.ToString());
            lines[5] = AddItemName(insertTabs, 5, Data.Weight.ToString());
            lines[6] = AddItemName(insertTabs, 6, Data.ABP1[0].ToString() + ABPseparator + Data.ABP1[1].ToString());
            lines[7] = AddItemName(insertTabs, 7, Data.ABP2[0].ToString() + ABPseparator + Data.ABP2[1].ToString());
            lines[8] = AddItemName(insertTabs, 8, Data.ABP3[0].ToString() + ABPseparator + Data.ABP3[1].ToString());
            lines[9] = AddItemName(insertTabs, 9, Data.ABP4[0].ToString() + ABPseparator + Data.ABP4[1].ToString());
            lines[10] = AddItemName(insertTabs, 10, Data.Pulse.ToString());
            return lines;
        }

        public void Save(ApexConfig cfg)
        {
            string[] lines = GetRecInfo(true);
            System.IO.File.WriteAllLines(FileName, lines, Encoding.Default);
            string[] snames = new string[1];
            for (int i = 0; i < ApexConstants.NumOfChannels + 1; i++)
            {
                snames[0] = snames[0] + ColumnNames[i] + Convert.ToChar(9);
            }
            File.AppendAllLines(FileName, snames, Encoding.Default);
        }

        private string AddItemName(bool tabs, int num, string s)
        {
            char c;
            if (tabs) 
            {
                c = Convert.ToChar(9);
            }
            else 
            {
                c = Convert.ToChar(0x20);
            }
            return Data.ItemNames[num]+c+s;
        }

        private string RemoveItemName(string s)
        {
            int pos9 = s.IndexOf(Convert.ToChar(9));
            return s.Substring(pos9+1);
        }
        
    }

}
