using Common;
using System;

namespace Zad1
{
    public class ParameterConfiguration
    {
        public ParameterConfiguration()
        {
            Modulo = new[] { 10, 1000, 100000 };
            PrimeNumber = CreatorPrime.GetNextPrime(Settings.MaxDataCount);

            var random = new Random(0);
            A = random.Next(PrimeNumber - 1) + 1;
            B = random.Next(PrimeNumber);
        }

        public int[] Modulo { get; }
        public int A { get; }
        public int B { get; }
        public int PrimeNumber { get; }
    }
}
