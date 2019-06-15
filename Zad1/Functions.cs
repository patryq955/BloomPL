using Common.Creators;
using Common.HashFunctions;
using System;
using System.Linq;

namespace Zad1
{
    public class Functions
    {
        private readonly ParameterConfiguration _parameterConfiguration;
        private readonly ResultFactory _resultCreator;

        public Functions(ParameterConfiguration parameterConfiguration, ResultFactory resultFactory)
        {
            _parameterConfiguration = parameterConfiguration;
            _resultCreator = resultFactory;
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
            Calculate(modulo, new CreatorBasic());
            Calculate(modulo, new CreatorEven());
            Calculate(modulo, new CreatorOdd());
            Calculate(modulo, new CreatorRawMsdc());
        }

        private void Calculate(long modulo, ICreator creator)
        {
            var simpleHashFunctionsResult = _resultCreator.CreateResult(creator, new BaseFunction(), modulo);
            var advancedHashFunctionsResult = _resultCreator.CreateResult(creator,
                new ExtendedFunction(_parameterConfiguration.A, _parameterConfiguration.B,
                    _parameterConfiguration.PrimeNumber), modulo);

            Printer(modulo, creator, simpleHashFunctionsResult, advancedHashFunctionsResult);
        }

        private static void Printer(long modulo, ICreator creator, Result simpleHashFunctionsResult, Result advancedHashFunctionsResult)
        {
            Console.WriteLine($"{creator}");
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
