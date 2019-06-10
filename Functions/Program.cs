using System;

using Common;
using Common.DataGenerators;
using Common.HashFunctions;

namespace Functions
{
    internal static class Program
    {
        private static int A;
        private static int B;
        private static int PrimeNumber;

        private static readonly Benchmark Benchmark = new Benchmark();

        private static void Main()
        {
            PrimeNumber = PrimeNumberGenerator.GetNextPrime(Settings.MaxDataCount);

            var random = new Random(0);

            A = random.Next(PrimeNumber - 1) + 1;
            B = random.Next(PrimeNumber);

            RunBenchmark();

            Console.Out.Dispose();
        }

        private static void RunBenchmark()
        {
            RunBenchmark(10);
            RunBenchmark(1000);
            RunBenchmark(100000);
        }

        private static void RunBenchmark(long modulo)
        {
            RunBenchmark(modulo, new BasicGenerator());
            RunBenchmark(modulo, new EvenGenerator());
            RunBenchmark(modulo, new OddGenerator());
            RunBenchmark(modulo, new RawMsdcGenerator());
        }

        private static void RunBenchmark(long modulo, IDataGenerator dataGenerator)
        {
            var simpleHashFunctionResult = Benchmark.Run(dataGenerator, new SimpleHashFunction(), modulo);
            var advancedHashFunctionResult = Benchmark.Run(dataGenerator, new AdvancedHashFunction(A, B, PrimeNumber), modulo);

            Printer.PrintResults(modulo, dataGenerator, simpleHashFunctionResult, advancedHashFunctionResult);
        }
    }
}