using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Polycardio2
{
    public class ApexConfig
    {
        public string DataDir { get; set; } 
        public int DataFileNum { get; set; }
        public bool CreateTextFile { get; set; } 
        public bool Maximized { get; set; }  
        public int WindowWidth { get; set; } 
        public int WindowHeight { get; set; } 
        public byte PressLevel1 { get; set; }  
        public byte PressLevel2 { get; set; }
        public byte PressLevel3 { get; set; }
        public byte PressLevel4 { get; set; }
        public int RecordLength { get; set; }
        public bool FilterOn { get; set; }
        public int StartDelay { get; set; }
        public bool[] VisibleGraphs { get; set; }
        public int[] ChannelsMaxSize {  get; set; }

        public ApexConfig()
        {
            VisibleGraphs = new bool[ApexConstants.NumOfChannels];
            DataDir = Directory.GetCurrentDirectory();
            DataFileNum = 0;
            CreateTextFile = true;
            Maximized = true;
            WindowWidth = 800;
            WindowHeight = 500;
            PressLevel1 = 50;
            PressLevel2 = 50;
            PressLevel3 = 50;
            PressLevel4 = 50;
            RecordLength = 10;
            FilterOn = true;
            StartDelay = 10;
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                VisibleGraphs[i] = true;
            }
            ChannelsMaxSize = [60000, 60000, 5000, 5000, 40000, 40000, 40000, 40000, 4000, 5000, 5000 ];

        }

        public static ApexConfig GetConfig()
        {
            ApexConfig ac;
            JsonSerializer serializer = new JsonSerializer();
                try
                {
                    using (StreamReader sr = new StreamReader(ApexConstants.ConfigFileName))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        ac = (ApexConfig)serializer.Deserialize(reader, typeof(ApexConfig));
                        return ac;
                    }
                }
                catch (Exception)
                {
                    ac = new ApexConfig();
                    SaveConfig(ac);
                    return ac;
                }
        }

    public static void SaveConfig(ApexConfig cfg)
    {
        // Использование конструкции 'using' автоматически закроет и очистит sw и writer
        using (StreamWriter sw = new StreamWriter(ApexConstants.ConfigFileName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            // Включаем красивое форматирование (отступы и переносы строк)
            writer.Formatting = Formatting.Indented;

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, cfg);
        }
    }


}
}
