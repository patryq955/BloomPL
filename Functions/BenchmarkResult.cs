using System;
using System.Collections.Generic;

using Common.HashFunctions;

namespace Functions
{
    internal sealed class BenchmarkResult
    {
        internal IHashFunction HashFunction { get; set; }

        internal TimeSpan Time { get; set; }

        internal Entropy Entropy { get; set; }

        internal double MeanSquaredError { get; set; }

        internal IEnumerable<KeyValuePair<long,long>> MappedValuesByKeys { get; set; }
    }

    internal sealed class Entropy
    {
        internal double Optimal { get; set; }

        internal double Actual { get; set; }

        internal double Difference => Math.Round(Optimal - Actual, 2);
    }
}