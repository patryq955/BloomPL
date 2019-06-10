using Common;
using Common.Bloom;
using Common.DataGenerators;

namespace Zad2
{
    class Program
    {
        static void Main()
        {
            var config = new ParameterConfiguration();
            var filterBloom = new FilterBloom(config);

            filterBloom.Run(new RandomGenerator(config.N, Settings.MaxDataCount));
        }
    }
}
