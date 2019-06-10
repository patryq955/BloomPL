using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DataGenerators
{
    public sealed class RandomGenerator : IDataGenerator
    {
        private readonly int _count;
        private readonly int _maxValue;

        public RandomGenerator(int count, int maxValue)
        {
            _count = count;
            _maxValue = maxValue;
        }

        public IEnumerable<int> Generate()
        {
            var random = new Random(0);

            return Enumerable.Range(0, _count).Select(_ => random.Next(_maxValue));
        }
    }
}