using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace lb4.Loggers
{
    public class ConsoleLogger
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

        static ConsoleLogger()
        {
            var configs = new ConfigurationBuilder()
                .AddJsonFile(ConfigFilePath, optional: true, reloadOnChange: true)
                .Build();

            var languageString = configs.GetSection("language").ToString();

            language = languageString switch
            {
                "eng" => Language.English,
                "ru" => Language.Russian,
                _ => throw new ArgumentException("Configs file is incorrect")
            };

            if (language == Language.Russian)
                Console.OutputEncoding = Encoding.UTF8;
        }
        
        public static void FailRequest()
        {
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
        
        private static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}