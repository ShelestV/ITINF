using System;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace lb4.Loggers
{
    public static class ConsoleLogger
    {
         private enum Language
        {
            Russian,
            English
        }

        private const string ConfigFilePath = "appsettings.json";
        
        private static Language language;
        
        private const ConsoleColor GREEN = ConsoleColor.Green;
        private const ConsoleColor RED = ConsoleColor.Red;
        private const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

        private static bool isInit = false;
        
        public static void Init()
        {
            var configs = new ConfigurationBuilder()
                .AddJsonFile(ConfigFilePath, optional: true, reloadOnChange: true)
                .Build();

            var languageString = configs.GetSection("language").Value;

            language = languageString switch
            {
                "eng" => Language.English,
                "ru" => Language.Russian,
                _ => throw new ArgumentException("Configs file is incorrect")
            };

            if (language == Language.Russian)
                Console.OutputEncoding = Encoding.UTF8;

            isInit = true;
        }

        public static void FailRequest()
        {
            if (!isInit) Init();
            
            switch (language)
            {
                case Language.English:
                    ChangeColor(RED);
                    Console.WriteLine("Request is failed!");
                    ChangeColor(DEFAULT_COLOR);
                    break;
                case Language.Russian:
                    Console.WriteLine("Запрос отклонён!");
                    break;
            }
        }

        public static void SetRequest(int channelIndex, double timeBusyTo)
        {
            if (!isInit) Init();
            
            switch (language)
            {
                case Language.English:
                    ChangeColor(GREEN);
                    Console.WriteLine((channelIndex + 1) + "-channel is busy up to " + timeBusyTo);
                    ChangeColor(DEFAULT_COLOR);
                    break;
                case Language.Russian:
                    Console.WriteLine((channelIndex + 1) + "-канал занят до " + timeBusyTo);
                    break;
            }
        }

        public static void FreeChannel(int channelIndex, double releaseTime)
        {
            if (!isInit) Init();
            
            switch (language)
            {
                case Language.English :
                    Console.WriteLine((channelIndex + 1) + "-channel is free at " + releaseTime);
                    break;
                case Language.Russian :
                    Console.WriteLine((channelIndex + 1) + "-канал освободился в " + releaseTime);
                    break;
            }
        }

        public static void Info(double z, double responseTime, double time, int channelIndex)
        {
            if (!isInit)
            {
                Init();
                Console.WriteLine("Z\tResp\tTime\tFree\tNumber of channel");
            }
            
            Console.WriteLine($"{Math.Round(1000 * z) / 1000}\t{Math.Round(1000 * responseTime) / 1000}\t{Math.Round(1000 * time) / 1000}\t{Math.Round(1000 * (time + responseTime)) / 1000}\t{channelIndex + 1}");
        }

        public static void Reject()
        {
            switch (language)
            {
                case Language.English:
                    ChangeColor(RED);
                    Console.WriteLine("Reject");
                    ChangeColor(ConsoleColor.White);
                    break;
                case Language.Russian:
                    Console.WriteLine("Отклонено");
                    break;
            }
        }
        
        private static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}