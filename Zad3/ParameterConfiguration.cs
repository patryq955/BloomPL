using Common.Bloom;

namespace Zad3
{
    internal class ParameterConfiguration : IParameterConfiguration
    {
        public ParameterConfiguration(int n)
        {
            Factor = 8;
            NumberHashFunctions = 1;
            N = n;
        }

        public int N { get; }
        public int NumberHashFunctions { get; }
        public int Factor { get; }
        public int FilterSize => N * Factor;
    }
}
