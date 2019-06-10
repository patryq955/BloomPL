using Common.Bloom;
using Common.DataGenerators;
using System.Linq;

namespace Zad3
{
    class Program
    {
        static void Main()
        {
            var generator = new RandomMsdcGenerator();
            var config = new ParameterConfiguration(generator.Generate().Count());

            var filter = new FilterBloom(config);
            filter.Run(generator);
        }
    }
}
