using Common.Creators;
using Common.HashFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common.Bloom
{
    public class FilterBloom
    {
        private readonly IParameterConfiguration _parameterConfiguration;
        private readonly int _prime;
        private readonly BitArray _vector;
        private readonly List<IFunction> _hashFunctions;

        private HashSet<int> _data;

        public FilterBloom(IParameterConfiguration parameterConfiguration)
        {
            _parameterConfiguration = parameterConfiguration;

            _vector = new BitArray(_parameterConfiguration.FilterSize);
            _hashFunctions = new List<IFunction>();
            _prime = CreatorPrime.GetNextPrime(Settings.MaxDataCount);
            GenerateHashFunctions();
        }

        public void Run(ICreator creator)
        {
            var numbers = creator.Create().ToList();

            var referenceFilterStopwatch = Stopwatch.StartNew();

            _data = new HashSet<int>(numbers);

            referenceFilterStopwatch.Stop();

            var bloomFilterStopwatch = Stopwatch.StartNew();

            foreach (var number in _data)
            {
                AddElement(number);
            }

            bloomFilterStopwatch.Stop();

            var result = new Result()
            {
                BloomFilterMemory = _parameterConfiguration.FilterSize * sizeof(bool),
                BloomFilterTime = bloomFilterStopwatch.Elapsed,
                ReferenceFilterMemory = _data.Count* sizeof(int) * 8,
                ReferenceFilterTime = referenceFilterStopwatch.Elapsed
            };

            ShowFilterBloom(result);
        }

        private void ShowFilterBloom(Result result)
        {
            int truePositive = 0, falsePositive = 0, trueNegative = 0, falseNegative = 0;

            for (var i = 0; i < Settings.MaxDataCount; i++)
            {
                if (Contains(i) && _data.Contains(i))
                {
                    truePositive++;
                }
                else if (!Contains(i) && !_data.Contains(i))
                {
                    trueNegative++;
                }
                else if (!Contains(i) && _data.Contains(i))
                {
                    falseNegative++;
                }
                else if (Contains(i) && !_data.Contains(i))
                {
                    falsePositive++;
                }
            }

            WriteLeft($"TP = {truePositive} ");
            WriteRight($"TPR {(double)truePositive / (double)(truePositive + falseNegative)}");

            WriteLeft($"TN = {trueNegative}");
            WriteRight($"TNR {(double)trueNegative / (double)(trueNegative + falsePositive)}");

            WriteLeft($"FN = {falseNegative}");
            WriteRight($"FNR {(double)falseNegative / (double)(truePositive + falseNegative)}");

            WriteLeft($"FP = {falsePositive}");
            WriteRight($"FPR {(double)falsePositive / (double)(trueNegative + falsePositive)}");
            NewLine();

            Console.WriteLine($"Expected FP: {CalculateTheoryFp()}");
            NewLine();

            Console.WriteLine($"Czas filtru referencyjnego = {result.ReferenceFilterTime}");
            Console.WriteLine($"Czas filtru Blooma = {result.BloomFilterTime}");
            NewLine();

            Console.WriteLine($"Pamięć filtru referencyjnego = {result.ReferenceFilterMemory}");
            Console.WriteLine($"Pamięć filtru Blooma = {result.BloomFilterMemory}");
        }

       

        private bool Contains(int value)
        {
            var found = true;

            foreach (var function in _hashFunctions)
            {
                var position = (int)function.Hash(value, _parameterConfiguration.FilterSize);

                if (_vector.Get(position))
                {
                    continue;
                }

                found = false;
                break;
            }

            return found;
        }

        private void AddElement(int value)
        {
            foreach (var function in _hashFunctions)
            {
                var position = (int)function.Hash(value, _parameterConfiguration.FilterSize);
                _vector.Set(position, true);
            }
        }

        private void GenerateHashFunctions()
        {
            var rand = new Random(0);

            for (long i = 0; i < _parameterConfiguration.NumberHashFunctions; i++)
            {
                long a = rand.Next(Settings.MaxDataCount - 1) + 1;
                long b = rand.Next(_prime);

                _hashFunctions.Add(new ExtendedFunction(a, b, _prime));
            }
        }

        private double CalculateTheoryFp() =>
           Math.Pow(1 -
                        Math.Exp(-_parameterConfiguration.NumberHashFunctions * (double)_parameterConfiguration.N / _parameterConfiguration.FilterSize),
                       _parameterConfiguration.NumberHashFunctions
                   );




        private static void WriteLeft(string text) => Console.Write("{0,-50}", text);

        private static void WriteRight(string text) => Console.WriteLine(text);

        private static void NewLine() => Console.WriteLine();

    }
}
