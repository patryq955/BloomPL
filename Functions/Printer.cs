using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Common.DataGenerators;
using Common.HashFunctions;

namespace Functions
{
    internal static class Printer
    {
        private const string OutputFilePath = "functions_out.txt";

        private static readonly IDictionary<Type, string> GeneratorDescriptions = new Dictionary<Type, string>();
        private static readonly IDictionary<Type, string> HashFunctionDescriptions = new Dictionary<Type, string>();

        static Printer()
        {

            HashFunctionDescriptions.Add(typeof(SimpleHashFunction), "");
            HashFunctionDescriptions.Add(typeof(AdvancedHashFunction), "");

            //var streamWriter = new StreamWriter(new FileStream(OutputFilePath, FileMode.Create))
            //{
            //    AutoFlush = true
            //};

            //Console.SetOut(streamWriter);
            //Console.SetError(streamWriter);
        }

        internal static void PrintResults(long modulo, IDataGenerator dataGenerator, BenchmarkResult simpleResult, BenchmarkResult advancedResult)
        {
            WriteCenter($"*** M = {modulo} *** ");
            WriteCenter($"*** {dataGenerator} ***");

            NewLine();

            WriteLeft($"*** {simpleResult.HashFunction} ***");
            WriteRight($"*** {advancedResult.HashFunction} ***");

            NewLine();

            WriteLeft($"Czas = {simpleResult.Time}");
            WriteRight($"Czas = {advancedResult.Time}");

            WriteLeft($"Błąd średniokwadratowy = {simpleResult.MeanSquaredError}");
            WriteRight($"Błąd średniokwadratowy = {advancedResult.MeanSquaredError}");

            WriteLeft($"E* = {simpleResult.Entropy.Optimal}, E = {simpleResult.Entropy.Actual}, E* - E = {simpleResult.Entropy.Difference}");
            WriteRight($"E* = {advancedResult.Entropy.Optimal}, E = {advancedResult.Entropy.Actual}, E* - E = {advancedResult.Entropy.Difference}");

            NewLine();

            WriteCenter("Liczba zmapowanych wartości kluczy do wartości funkcji mieszającej z zakresu od 0 do 9");

            NewLine();

            var mappedValuesByKeys = simpleResult.MappedValuesByKeys.Join(advancedResult.MappedValuesByKeys, x => x.Key, x => x.Key, (x, y) => new { Simple = x, Advanced = y });

            foreach (var pair in mappedValuesByKeys)
            {
                WriteLeft($"Wartość = {pair.Simple.Key}, zmapowane klucze = {pair.Simple.Value}");
                WriteRight($"Wartość = {pair.Simple.Key}, zmapowane klucze = {pair.Simple.Value}");
            }

            WriteCenter("****************************************************************************************************");
            NewLine();
            WriteCenter("****************************************************************************************************");
        }

        private static void WriteCenter(string text) => Console.WriteLine(text);

        private static void WriteLeft(string text) => Console.Write("{0,-50}", text);

        private static void WriteRight(string text) => Console.WriteLine(text);

        private static void NewLine() => Console.WriteLine();
    }
}
