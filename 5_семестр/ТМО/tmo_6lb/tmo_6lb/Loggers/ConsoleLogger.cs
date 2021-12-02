using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace tmo_6lb.Loggers
{
	class ConsoleLogger
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

        public static void SetRequestFromQueue(int channelIndex, double timeBusyTo)
        {
            if (!isInit) Init();

            switch (language)
            {
                case Language.English:
                    ChangeColor(GREEN);
                    Console.WriteLine((channelIndex + 1) + "-channel is busy up to " + timeBusyTo + " by request from queue");
                    ChangeColor(DEFAULT_COLOR);
                    break;
                case Language.Russian:
                    Console.WriteLine((channelIndex + 1) + "-канал занят до " + timeBusyTo + " запросом из очереди");
                    break;
            }
        }

        public static void FreeChannel(int channelIndex, double releaseTime)
        {
            if (!isInit) Init();

            switch (language)
            {
                case Language.English:
                    Console.WriteLine((channelIndex + 1) + "-channel is free at " + releaseTime);
                    break;
                case Language.Russian:
                    Console.WriteLine((channelIndex + 1) + "-канал освободился в " + releaseTime);
                    break;
            }
        }

        public static void Reject()
        {
            switch (language)
            {
                case Language.English:
                    ChangeColor(RED);
                    Console.WriteLine("Reject");
                    ChangeColor(DEFAULT_COLOR);
                    break;
                case Language.Russian:
                    Console.WriteLine("Отклонено");
                    break;
            }
        }

        public static void AddRequestToQueue()
		{
            switch(language)
			{
                case Language.English:
                    ChangeColor(RED);
                    Console.WriteLine("Request has been added to queue");
					ChangeColor(DEFAULT_COLOR);
                    break;
                case Language.Russian:
                    Console.WriteLine("Запрос был добавлен в очередь");
                    break;
			}
		}

        public static void EmptyLine()
        {
            Console.WriteLine();
        }

        private static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
