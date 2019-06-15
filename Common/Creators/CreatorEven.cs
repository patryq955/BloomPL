using System.Collections.Generic;

namespace Common.Creators
{
    public sealed class CreatorEven : ICreator
    {
        private const long Max = (2 * Settings.MaxDataCount) - 1;

        public IEnumerable<int> Create()
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