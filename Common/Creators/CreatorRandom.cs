using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Creators
{
    public sealed class CreatorRandom : ICreator
    {
        private readonly int _count;
        private readonly int _maxValue;

        public CreatorRandom(int count, int maxValue)
        {
            _count = count;
            _maxValue = maxValue;
        }

        public IEnumerable<int> Create()
        {
            var random = new Random(0);

            return Enumerable.Range(0, _count).Select(_ => random.Next(_maxValue));
        }
    }
}