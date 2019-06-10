using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Common;
using Common.DataGenerators;
using Common.HashFunctions;

namespace BloomFilter
{
    internal sealed class BloomFilterAlgorithm
    {
        private readonly int _maxValue;
        private readonly int _numberOfHashFunctions;
        private readonly int _filterSize;
        private readonly int _primeNumber;
        private readonly IList<IHashFunction> _hashFunctions;

        public BloomFilterAlgorithm(int maxValue, int numberOfHashFunctions, int filterSize)
        {
            _maxValue = maxValue;
            _filterSize = filterSize;
            _primeNumber = PrimeNumberGenerator.GetNextPrime(_maxValue);
            _numberOfHashFunctions = numberOfHashFunctions;
            _hashFunctions = new List<IHashFunction>();

            GenerateHashFunctions();
        }

        private void GenerateHashFunctions()
        {
            var rand = new Random(0);

            for (long i = 0; i < _numberOfHashFunctions; i++)
            {
                long a = rand.Next(_primeNumber - 1) + 1;
                long b = rand.Next(_primeNumber);

                _hashFunctions.Add(new AdvancedHashFunction(a, b, _primeNumber));
            }
        }

        public BloomFilterResult Run(IDataGenerator dataGenerator)
        {
            var vector = new BitArray(_filterSize);
            var numbers = dataGenerator.Generate().ToList();

            var referenceFilterStopwatch = Stopwatch.StartNew();

            var data = new HashSet<int>(numbers);

            referenceFilterStopwatch.Stop();

            var bloomFilterStopwatch = Stopwatch.StartNew();

            foreach (int number in data)
            {
                SetPositions(vector, number);
            }

            bloomFilterStopwatch.Stop();

            var result = CreateResult(data, vector);

            result.ReferenceFilterTime = referenceFilterStopwatch.Elapsed;
            result.BloomFilterTime = bloomFilterStopwatch.Elapsed;
            result.ReferenceFilterMemory = data.Count * sizeof(int);
            result.BloomFilterMemory = _filterSize * sizeof(bool);

            return result;
        }

        private void SetPositions(BitArray vector, int number)
        {
            foreach (var function in _hashFunctions)
            {
                vector.Set((int)function.Calculate(number, _filterSize), true);
            }
        }

        private BloomFilterResult CreateResult(HashSet<int> data, BitArray vector)
        {
            long truePositive = 0, falsePositive = 0, trueNegative = 0, falseNegative = 0;

            for (int i = 0; i < _maxValue; i++)
            {
                bool isInVector = Contains(vector, i);
                bool isInData = data.Contains(i);

                if (isInVector && isInData)
                {
                    truePositive++;
                }
                else if (!isInVector && !isInData)
                {
                    trueNegative++;
                }
                else if (!isInVector && isInData)
                {
                    falseNegative++;
                }
                else if (isInVector && !isInData)
                {
                    falsePositive++;
                }
            }

            return new BloomFilterResult
            {
                TruePositive = truePositive,
                TrueNegative = trueNegative,
                FalsePositive = falsePositive,
                FalseNegative = falseNegative,
                ExpectedFalsePositive = Math.Pow(1 - Math.Exp(-_numberOfHashFunctions * (double)data.Count() / _filterSize), _numberOfHashFunctions)
            };
        }

        private bool Contains(BitArray vector, long value)
        {
            bool found = true;

            foreach (var function in _hashFunctions)
            {
                long position = function.Calculate(value, _filterSize);

                if (vector.Get((int)position))
                {
                    continue;
                }

                found = false;

                break;
            }

            return found;
        }
    }
}
