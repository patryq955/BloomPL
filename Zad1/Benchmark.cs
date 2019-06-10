using Common.DataGenerators;
using Common.HashFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zad1
{
    public class Benchmark
    {
        internal Result Run(IDataGenerator dataGenerator, IHashFunction hashFunction, long modulo)
        {
            var data = dataGenerator.Generate().ToList();
            var mappedValues = new Dictionary<long, long>();

            var stopWatch = Stopwatch.StartNew();

            foreach (long number in data)
            {
                var value = hashFunction.Calculate(number, modulo);

                if (mappedValues.ContainsKey(value))
                {
                    mappedValues[value]++;
                }
                else
                {
                    mappedValues.Add(value, 1);
                }
            }

            stopWatch.Stop();

            return CreateResult(stopWatch.Elapsed, mappedValues.OrderBy(x => x.Key).ToList(), modulo, hashFunction);
        }

        private Result CreateResult(TimeSpan time, List<KeyValuePair<long, long>> mappedValues, long modulo, IHashFunction hashFunction)
        {
            return new Result()
            {
                Time = time,
                HashFunction = hashFunction,
                Entropy = CalculateEntropy(mappedValues, modulo),
                MeanSquaredError = CalculateMeanSquaredError(mappedValues, modulo),
                MappedValuesByKeys = mappedValues.Where(x => x.Key >= 0 && x.Key <= 9).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value)
            };
        }

        private Entropy CalculateEntropy(List<KeyValuePair<long, long>> mappedValues, long modulo)
        {
            double sum = 0;
            var count = mappedValues.Sum(x => x.Value);

            for (long i = 0; i < modulo; i++)
            {
                var probability = GetProbability(mappedValues, i, count);

                if (Math.Abs(probability) > 0)
                {
                    sum += probability * Math.Log10(probability);
                }
            }

            return new Entropy { Actual = Math.Round(-sum, 2), Optimal = Math.Round(-Math.Log10(1 / (double)modulo), 2) };
        }

        private double CalculateMeanSquaredError(List<KeyValuePair<long, long>> mappedValues, long modulo)
        {
            double sum = 0;
            var count = mappedValues.Sum(x => x.Value);

            for (long i = 0; i < modulo; i++)
            {
                var probability = GetProbability(mappedValues, i, count);

                sum += Math.Pow(probability - 1 / (double)modulo, 2);
            }

            return Math.Round(sum / modulo, 2);
        }

        private double GetProbability(List<KeyValuePair<long, long>> mappedValues, long value, long count)
        {
            int index = mappedValues.BinarySearch(new KeyValuePair<long, long>(value, 0), new Comparer());

            if (index < 0)
            {
                return 0;
            }

            return mappedValues[index].Value / (double)count;
        }

        private class Comparer : Comparer<KeyValuePair<long, long>>
        {
            public override int Compare(KeyValuePair<long, long> x, KeyValuePair<long, long> y)
            {
                if (x.Key < y.Key)
                {
                    return -1;
                }

                return x.Key == y.Key ? 0 : 1;
            }
        }
    }
}
