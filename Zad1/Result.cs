using Common.HashFunctions;
using System;
using System.Collections.Generic;

namespace Zad1
{
    internal sealed class Result
    {
        internal IHashFunction HashFunction { get; set; }

        internal TimeSpan Time { get; set; }

        internal Entropy Entropy { get; set; }

        internal double MeanSquaredError { get; set; }

        internal IEnumerable<KeyValuePair<long, long>> MappedValuesByKeys { get; set; }
    }

    internal sealed class Entropy
    {
        internal double Optimal { get; set; }

        internal double Actual { get; set; }

        internal double Difference => Optimal - Actual;
    }
}
