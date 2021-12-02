using System;
using System.Collections;
using System.Collections.Generic;
using tmo_6lb.Loggers;
using tmo_6lb.Streams;
using tmo_6lb.Streams.Enums;
using tmo_6lb.Streams.Helpers;

namespace tmo_6lb.Collections
{
	class ResponseStreamCollection : IEnumerable<PoissonDemandStream>
	{
        private readonly int size;
        private readonly PoissonDemandStream[] streams;
        private readonly bool[] isBusy;
        private readonly double[] releaseTimes;

        private Queue<double> requestQueue = new Queue<double>();

        public ICollection<Period> QueuePeriods { get; } = new List<Period>();
        private double lastTime = 0.0;
        public int ToQueueCount { get; private set; } = 0;
        public int RequestsCount { get; private set; } = 0;

        public int Count => streams.Length;
        public PoissonDemandStream this[int index] { get => streams[index]; }

        public ResponseStreamCollection(int size = 0)
        {
            this.size = size;

            streams = new PoissonDemandStream[size];
            isBusy = new bool[size];
            releaseTimes = new double[size];
        }

        public void InitStreams(double streamParameter, double startTime)
        {
            for (var i = 0; i < size; ++i)
                streams[i] = new PoissonDemandStream(streamParameter, StreamType.Response, startTime);
            lastTime = startTime;
        }

        public void SetRequest(double time)
        {
            RequestsCount++;
            FreeChannels(time);

            for (var i = 0; i < size; ++i)
            {
                if (isBusy[i]) continue;


                streams[i].ActivityPeriods.Add(new Period(streams[i].LastTime, time, 0)); // period of free
                releaseTimes[i] = time + streams[i].GetNextWaitingTime();
                streams[i].ActivityPeriods.Add(new Period(time, releaseTimes[i], 1));
                streams[i].LastTime = releaseTimes[i];

                isBusy[i] = true;
                ConsoleLogger.SetRequest(i, releaseTimes[i]);

                return;
            }
            requestQueue.Enqueue(time);
            ToQueueCount++;

            QueuePeriods.Add(new Period(lastTime, time, requestQueue.Count));
            lastTime = time;

            ConsoleLogger.AddRequestToQueue();
        }

        private void FreeChannels(double time)
        {
            for (var i = 0; i < size; ++i)
            {
                if (!isBusy[i] || !(releaseTimes[i] <= time)) continue;

                ConsoleLogger.FreeChannel(i, releaseTimes[i]);
                isBusy[i] = false;
                releaseTimes[i] = 0.0;
                try
                {
                    requestQueue.Dequeue();
                    QueuePeriods.Add(new Period(lastTime, time, requestQueue.Count));
                    lastTime = time;
                    streams[i].ActivityPeriods.Add(new Period(streams[i].LastTime, releaseTimes[i], 0)); // period of free
                    releaseTimes[i] = time + streams[i].GetNextWaitingTime();
                    streams[i].ActivityPeriods.Add(new Period(time, releaseTimes[i], 1));
                    streams[i].LastTime = releaseTimes[i];
                    isBusy[i] = true;
                    ConsoleLogger.SetRequestFromQueue(i, releaseTimes[i]);
                }
                catch (InvalidOperationException) { }
            }
        }

        public ICollection<Period> GetActivityPeriods(int index)
		{
            return streams[index].ActivityPeriods;
		}

		public IEnumerator<PoissonDemandStream> GetEnumerator()
		{
            foreach (var stream in streams)
                yield return stream;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
            foreach (var stream in streams)
                yield return stream;
		}
	}
}
