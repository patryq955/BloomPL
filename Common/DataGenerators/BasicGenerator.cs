using System.Collections.Generic;

namespace Common.DataGenerators
{
    public sealed class BasicGenerator : IDataGenerator
    {
        private const long Max = Settings.MaxDataCount - 1;

        public IEnumerable<int> Generate()
        {
            for (var number = 0; number <= Max; number++)
            {
                yield return number;
            }
        }

        public override string ToString() => "Kolejne liczby z zakresu od 0 do 10^8 - 1";
    }
}
