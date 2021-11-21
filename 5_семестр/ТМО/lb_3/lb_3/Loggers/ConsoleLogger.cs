using System;

namespace lb_3.Loggers
{
    internal static class ConsoleLogger
    {
        private const ConsoleColor GREEN = ConsoleColor.Green;
        private const ConsoleColor RED = ConsoleColor.Red;
        private const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;
        
        public static void FailRequest()
        {
            ChangeColor(RED);
            Console.WriteLine("Request is failed!");
            ChangeColor(DEFAULT_COLOR);
        }

        public static void SetRequest(int channelIndex, double timeBusyTo)
        {
            ChangeColor(GREEN);
            Console.WriteLine((channelIndex + 1) + "-channel is busy up to " + timeBusyTo);
            ChangeColor(DEFAULT_COLOR);
        }

        public static void FreeChannel(int channelIndex, double releaseTime)
        {
            Console.WriteLine((channelIndex + 1) + "-channel is free at " + releaseTime);
        }
        
        private static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}