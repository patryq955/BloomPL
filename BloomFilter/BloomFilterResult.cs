using System;

namespace BloomFilter
{
    internal sealed class BloomFilterResult
    {
        internal long TruePositive { get; set; }
        internal long TrueNegative { get; set; }
        internal long FalsePositive { get; set; }
        internal long FalseNegative { get; set; }
        internal double ExpectedFalsePositive { get; set; }

        internal TimeSpan BloomFilterTime { get; set; }

        internal TimeSpan ReferenceFilterTime { get; set; }

        internal int BloomFilterMemory { get; set; }

        internal int ReferenceFilterMemory { get; set; }

        internal double TruePositiveRate => (double)TruePositive / (TruePositive + FalseNegative);
        internal double TrueNegativeRate => (double)TrueNegative / (TrueNegative + FalsePositive);
        internal double FalsePositiveRate => (double)FalsePositive / (FalsePositive + TrueNegative);
        internal double FalseNegativeRate => (double)FalseNegative / (FalseNegative + TruePositive);
    }
}
