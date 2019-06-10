using System;

namespace Common.Bloom
{
    internal sealed class Result
    {
        internal TimeSpan BloomFilterTime { get; set; }

        internal TimeSpan ReferenceFilterTime { get; set; }

        internal int BloomFilterMemory { get; set; }

        internal int ReferenceFilterMemory { get; set; }
    }
}
