using System.IO;

using static System.Console;

namespace BloomFilter
{
    internal static class Printer
    {
        private const string OutputFilePath = "bloom_out.txt";

        static Printer()
        {
            var streamWriter = new StreamWriter(new FileStream(OutputFilePath, FileMode.Create))
            {
                AutoFlush = true
            };

            SetOut(streamWriter);
            SetError(streamWriter);
        }

        public static void Print(BloomFilterResult result, int numberCount, int numberOfHashFunctions)
        {
            WriteLine($"Wielkość zbioru = {numberCount}");
            WriteLine($"Liczba funkcji mieszających = {numberOfHashFunctions}");
            WriteLine();
            WriteLine($"Czas filtru referencyjnego = {result.ReferenceFilterTime}");
            WriteLine($"Czas filtru Blooma = {result.BloomFilterTime}");
            WriteLine();
            WriteLine($"Pamięć filtru referencyjnego = {result.ReferenceFilterMemory}");
            WriteLine($"Pamięć filtru Blooma = {result.BloomFilterMemory}");
            WriteLine();
            WriteLine($"TP = {result.TruePositive}, TPR = {result.TruePositiveRate:##.00}");
            WriteLine($"TN = {result.TrueNegative}, TNR = {result.TrueNegativeRate:##.00}");
            WriteLine($"FP = {result.FalsePositive}, FPR = {result.FalsePositiveRate:##.00}");
            WriteLine($"FN = {result.FalseNegative}, FNR = {result.FalseNegativeRate:##.00}");
            WriteLine($"Oczekiwana wartość FP = {result.ExpectedFalsePositive:##.00}");
            WriteLine();
        }
    }
}
