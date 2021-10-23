using lb_3.Generators.Abstract;

namespace lb_3.DemandStreams
{
    public class PoissonDemandStream
    {
        private readonly double streamParameter;
        private readonly IRandomGenerator randomGenerator;
    }
}