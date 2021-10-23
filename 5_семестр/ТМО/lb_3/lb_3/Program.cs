using lb_3.DemandStreams;

namespace lb_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int variant = 30;
            int group = 1;
            int numberOfChannels = 5; 
            
            double requestStreamParameter = 10.0 * group / (variant * numberOfChannels);
            double responseStreamParameter = 3.0 * (group + variant) / (variant * numberOfChannels);

            var requestStream = new PoissonDemandStream(requestStreamParameter);
            var responseStreams = new PoissonDemandStream[numberOfChannels];

            for (int i = 0; i < numberOfChannels; ++i)
            {
                responseStreams[i] = new PoissonDemandStream(responseStreamParameter);
            }
            
            
        }
    }
}