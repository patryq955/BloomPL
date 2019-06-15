using Common;
using Common.Bloom;
using Common.Creators;
using System;

namespace Zad2
{
    class Program
    {
        static void Main()
        {
            var config = new ParameterConfiguration();
            var filterBloom = new FilterBloom(config);

            filterBloom.Run(new CreatorRandom(config.N, Settings.MaxDataCount));

            Console.ReadLine();
        }
    }
}
