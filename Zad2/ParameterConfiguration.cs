using Common.Bloom;

namespace Zad2
{
    internal class ParameterConfiguration : IParameterConfiguration
    {
        public ParameterConfiguration()
        {
            N = 100_000;
            NumberHashFunctions = 1;
            Factor = 8;
        }

        public int N { get; }
        public int NumberHashFunctions { get; }
        public int Factor { get; }
        public int FilterSize => Factor * N;
    }
}
