using System;
using System.Collections.Generic;
using System.Linq;

namespace tmo_2lb
{
    class PoissonRequirementsStream
    {
        private readonly double streamParameter;
        private readonly List<double> r = new List<double>();
        private readonly List<double> z = new List<double>();
        private readonly List<double> requestReceipt = new List<double>();
        private readonly List<int> intervals = new List<int>();

        private readonly Dictionary<int, int> distributionParameters
            = new Dictionary<int, int>();

        private readonly double startTime;
        private readonly double endTime;
        private double intervalTime;

        private double expectedValue;
        private double modelStreamParameter;

        public List<int> Intervals => intervals;

        public double ModelStreamParameter => modelStreamParameter;

        public PoissonRequirementsStream(double streamParameter,
            int startTime,
            int endTime)
        {
            this.streamParameter = streamParameter;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public PoissonRequirementsStream(List<int> intervals,
            int variant,
            int startTime,
            int endTime)
        {
            this.intervals = intervals;
            this.startTime = startTime;
            this.endTime = endTime;
            intervalTime = (this.endTime - this.startTime) / intervals.Count;

            CalculateDistributionParameters();
            CalculateModelOfStreamParameter(variant);
        }

        // Variant as parameter is very strange
        // But the problem is also not quite mathematical
        public void CalculateStreamCharacteristics(IEnumerable<double> randomNumbers,
            int numberOfIntervals,
            int variant)
        {
            CalculateZ(randomNumbers);
            CalculateRequestReceipt();
            CalculateIntervals(numberOfIntervals);
            CalculateDistributionParameters();
            CalculateModelOfStreamParameter(variant);
        }

        public void CalculateZ(IEnumerable<double> randomNumbers)
        {
            foreach (double number in randomNumbers)
            {
                r.Add(number);
                z.Add(-(Math.Log(number) / streamParameter));
            }
        }

        public void CalculateRequestReceipt()
        {
            double time = startTime;
            int index = 0;
            while (time + z[index] < endTime)
            {
                time += z[index];
                requestReceipt.Add(time);
                ++index;
            }
        }

        public void CalculateIntervals(int numberOfIntervals)
        {
            intervalTime = (endTime - startTime) / numberOfIntervals;
            int requestReceiptIndex = 0;
            for (int i = 0; i < numberOfIntervals; ++i)
            {
                int requestOnInterval = 0;

                double start = GetIntervalTimeFromStartTime(i, intervalTime);
                double end = GetIntervalTimeFromStartTime(i + 1, intervalTime);

                if (IsIndexInRequestReceiptRange(requestReceiptIndex) &&
                    end >= requestReceipt[requestReceiptIndex])
                {
                    while (IsIndexInRequestReceiptRange(requestReceiptIndex) &&
                           IsOnInterval(start, end, requestReceipt[requestReceiptIndex]))
                    {
                        ++requestReceiptIndex;
                        ++requestOnInterval;
                    }
                }

                intervals.Add(requestOnInterval);
            }
        }

        private double GetIntervalTimeFromStartTime(int numberOfInterval,
            double parameterIntervalTime)
        {
            return startTime + numberOfInterval * parameterIntervalTime;
        }

        private bool IsIndexInRequestReceiptRange(int index)
        {
            return 0 <= index && index < requestReceipt.Count;
        }

        private bool IsOnInterval(double startIntervalTime,
            double endIntervalTime,
            double time)
        {
            return startIntervalTime <= time && time <= endIntervalTime;
        }

        public void CalculateDistributionParameters()
        {
            int requestNumberOnInterval = 0;

            for (int numberOfCheckIntervals = 0; numberOfCheckIntervals < intervals.Count;)
            {
                int numberOfIntervals = 0;
                foreach (var interval in intervals)
                {
                    if (interval == requestNumberOnInterval)
                    {
                        ++numberOfIntervals;
                        ++numberOfCheckIntervals;
                    }
                }

                distributionParameters.Add(requestNumberOnInterval, numberOfIntervals);
                ++requestNumberOnInterval;
            }
        }

        public void CalculateModelOfStreamParameter(int variant)
        {
            double preExpectedValue = distributionParameters.Select(par => par.Key * par.Value).Sum();
            expectedValue = preExpectedValue / (double)intervals.Count;
            modelStreamParameter = expectedValue / intervalTime;
        }

        public void OutputData()
        {
            Console.WriteLine("r\tz\tt");
            for (var i = 0; i < z.Count; ++i)
            {
                var requestReceiptString = i < requestReceipt.Count ? (Math.Round(1000 * requestReceipt[i]) / 1000).ToString() : "";
                Console.WriteLine($"{Math.Round(1000 * r[i]) / 1000}\t{Math.Round(1000 * z[i]) / 1000}\t{requestReceiptString}");
            }
        }

        public void OutputStreamCharacteristics()
        {
            OutputTimeData();
            OutputZ();
            OutputRequestReceipt();
            OutputIntervals();
            OutputDistributionParameters();
            OutputAmountOfEqualRequirementsOnInterval();
            OutputExpectedValue();
        }

        public void OutputTimeData()
        {
            Console.WriteLine("Start time: " + startTime);
            Console.WriteLine("End time: " + endTime);
            Console.WriteLine("Time difference: " + (endTime - startTime));
            Console.WriteLine();
        }

        public void OutputZ()
        {
            Console.WriteLine("Z:");
            foreach (double z in z)
                Console.WriteLine("\t" + z);
            Console.WriteLine();
        }

        public void OutputRequestReceipt()
        {
            Console.WriteLine("Requirements times: ");
            foreach (double time in requestReceipt)
                Console.WriteLine("\t" + time);
            Console.WriteLine();
        }

        public void OutputIntervals()
        {
            Console.WriteLine("Interval time: " + intervalTime);
            Console.WriteLine("Requirements on intervals");
            int i = 0;
            foreach (double interval in intervals)
                Console.WriteLine("\t" + ++i + ": " + interval);
            Console.WriteLine();
        }

        public void OutputDistributionParameters()
        {
            Console.WriteLine("Amount of equal requirements on intervals: ");
            foreach (var amount in distributionParameters)
                Console.WriteLine("\t" + amount.Key + ": " + amount.Value);
            Console.WriteLine();
        }

        public void OutputAmountOfEqualRequirementsOnInterval()
        {
            var amountOfEqualRequirementsOnInterval =
                distributionParameters.Select(key => key.Value).ToList();
            Console.WriteLine("Sum of amount: " + amountOfEqualRequirementsOnInterval.ToArray().Sum());
            Console.WriteLine();
        }

        public void OutputExpectedValue()
        {
            Console.WriteLine("Expected value: " + expectedValue);
            Console.WriteLine();
        }

        public double GetExpectedValue()
        {
            return intervals.Select(i => (double)i / intervals.Count).Sum();
        }

        public double GetDispersion()
        {
            double M = GetExpectedValue();
            return intervals.Select(i => Math.Pow(i - M, 2) / (intervals.Count - 1)).Sum();
        }
    }
}