using Common.DataGenerators;
using Common.HashFunctions;
using System;
using System.Linq;

namespace Zad1
{
    public class Functions
    {
        private readonly ParameterConfiguration _parameterConfiguration;
        private readonly ResultFactory _benchmark;

        public Functions(ParameterConfiguration parameterConfiguration, ResultFactory benchmark)
        {
            _parameterConfiguration = parameterConfiguration;
            _benchmark = benchmark;
        }

        public void Run()
        {
            Calculate();
        }

        private void Calculate()
        {
            foreach (var modulo in _parameterConfiguration.Modulo)
            {
                Calculate(modulo);
            }
        }

        private void Calculate(long modulo)
        {
            Calculate(modulo, new BasicGenerator());
            Calculate(modulo, new EvenGenerator());
            Calculate(modulo, new OddGenerator());
            Calculate(modulo, new RawMsdcGenerator());
        }

        private void Calculate(long modulo, IDataGenerator dataGenerator)
        {
            var simpleHashFunctionsResult = _benchmark.CreateResult(dataGenerator, new BaseFunction(), modulo);
            var advancedHashFunctionsResult = _benchmark.CreateResult(dataGenerator,
                new ExtendedFunction(_parameterConfiguration.A, _parameterConfiguration.B,
                    _parameterConfiguration.PrimeNumber), modulo);

            Printer(modulo, dataGenerator, simpleHashFunctionsResult, advancedHashFunctionsResult);
        }

        private static void Printer(long modulo, IDataGenerator dataGenerator, Result simpleHashFunctionsResult, Result advancedHashFunctionsResult)
        {
            Console.WriteLine($"{dataGenerator}");
            Console.WriteLine($"M = {modulo}");
            NewLine();
            NewLine();
            NewLine();


            WriteLeft($"{simpleHashFunctionsResult.HashFunction}");
            WriteRight($"{advancedHashFunctionsResult.HashFunction}");

            WriteLeft($"Czas: {simpleHashFunctionsResult.Time}");
            WriteRight($"Czas: {advancedHashFunctionsResult.Time}");

            WriteLeft($"Blad sredniokwadratowy: {simpleHashFunctionsResult.MeanSquaredError}");
            WriteRight($"Blad sredniokwadratowy: {advancedHashFunctionsResult.MeanSquaredError}");

            WriteLeft($"E* = {simpleHashFunctionsResult.Entropy.Optimal} E = {simpleHashFunctionsResult.Entropy.Actual} E* - E = {simpleHashFunctionsResult.Entropy.Difference}");
            WriteRight($"E* = {advancedHashFunctionsResult.Entropy.Optimal} E = {advancedHashFunctionsResult.Entropy.Actual} E* - E = {advancedHashFunctionsResult.Entropy.Difference}");
            NewLine();

            Console.WriteLine("Liczba zmapowanych wartości kluczy do wartości funkcji mieszającej z zakresu od 0 do 9");
            var mappedValuesByKeys = simpleHashFunctionsResult.MappedValuesByKeys.Join(advancedHashFunctionsResult.MappedValuesByKeys, x => x.Key, x => x.Key, (x, y) => new { Simple = x, Advanced = y });

            foreach (var pair in mappedValuesByKeys)
            {
                WriteLeft($"Wartość = {pair.Simple.Key}, zmapowane klucze = {pair.Simple.Value}");
                WriteRight($"Wartość = {pair.Advanced.Key}, zmapowane klucze = {pair.Advanced.Value}");
            }

            NewLine();
            NewLine();
        }

        private static void WriteLeft(string text) => Console.Write("{0,-50}", text);

        private static void WriteRight(string text) => Console.WriteLine(text);

        private static void NewLine() => Console.WriteLine();
    }
}
