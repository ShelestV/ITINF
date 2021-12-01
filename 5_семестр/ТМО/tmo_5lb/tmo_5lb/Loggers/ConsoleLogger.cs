using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace tmo_5lb.Loggers
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
                    ChangeColor(ConsoleColor.White);
                    break;
                case Language.Russian:
                    Console.WriteLine("Отклонено");
                    break;
            }
        }

        public static void QueueExistingProbability(double value)
		{
            switch (language)
			{
                case Language.English:
                    Console.WriteLine("Queue existing probability = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Вероятность наличия очереди = " + value);
                    break;
			}
		}

        public static void AllChannelsBusyProbability(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("All channels busy probability = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Вероятность того, что все каналы заняты = " + value);
                    break;
            }
        }

        public static void AvgNumberOfRequests(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage number of requests in system = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Среднее количество заявок в системе = " + value);
                    break;
            }
        }

        public static void AvgQueueLength(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage queue length = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Средняя длина очереди = " + value);
                    break;
            }
        }

        public static void AvgNumberOfFreeNodes(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage number of free channels = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Среденее количество свободных каналов = " + value);
                    break;
            }
        }

        public static void AvgNumberOfBusyNodes(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage number of busy channels = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Среднее количество занятых каналов = " + value);
                    break;
            }
        }

        public static void AvgWaitingTimeToServe(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage waiting time to serve for request = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Среднее время ожидания обслуживания для заявки = " + value);
                    break;
            }
        }

        public static void GeneralWaitingTimeForRequestInQueue(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("General waiting time for request in queue = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Общее время ожидания заявки в очереди = " + value);
                    break;
            }
        }

        public static void AvgTimeOfRequestInSystem(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage time of request in system = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Среднее время заявки в системе = " + value);
                    break;
            }
        }

        public static void SumTimeOfAllRequestsInSystem(double value)
        {
            switch (language)
            {
                case Language.English:
                    Console.WriteLine("Avarage sum time all requests is in system per time = " + value);
                    break;
                case Language.Russian:
                    Console.WriteLine("Сумарное время, которое в среднем проводят все заявки в системе, за еденицу времени = " + value);
                    break;
            }
        }

        public static void Pk(int k, double Pk)
		{
            Console.WriteLine($"k({k}): Pk = {Pk}");
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
