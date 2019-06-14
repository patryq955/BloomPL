using System.Collections.Generic;

namespace Common.DataGenerators
{
    public sealed class EvenGenerator : IDataGenerator
    {
        private const long Max = (2 * Settings.MaxDataCount) - 1;

        public IEnumerable<int> Generate()
        {
            for (var number = 0; number <= Max; number++)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }

        public override string ToString() => "Liczby parzyste z zakresu od 0 do 2 × 10^8 − 1";
    }
}