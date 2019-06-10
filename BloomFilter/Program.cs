using Common;
using Common.DataGenerators;
using System;
using System.Linq;

namespace BloomFilter
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            RunSecondTask();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            RunThirdTask();

            Console.Out.Dispose();
        }

        private static void RunSecondTask()
        {
            int factor = 8;
            int numberCount = 100_000;
            int numberOfHashFunctions = 1;
            int filterSize = numberCount * factor;

            var algorithm = new BloomFilterAlgorithm(Settings.MaxDataCount, numberOfHashFunctions, filterSize);

            var result = algorithm.Run(new RandomGenerator(numberCount, Settings.MaxDataCount));

            Printer.Print(result, numberCount, numberOfHashFunctions);
        }

        private static void RunThirdTask()
        {
            var generator = new RandomMsdcGenerator();

            int factor = 8;
            int numberCount = generator.Generate().Count();
            int numberOfHashFunctions = 1;
            int filterSize = numberCount * factor;

            var algorithm = new BloomFilterAlgorithm(Settings.MaxDataCount, numberOfHashFunctions, filterSize);

            var result = algorithm.Run(generator);

            Printer.Print(result, numberCount, numberOfHashFunctions);
        }
    }
}
