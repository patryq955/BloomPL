namespace Zad1
{
    class Program
    {
        static void Main()
        {
            var functions = new Functions(new ParameterConfiguration(), new Benchmark());

            functions.Run();
        }
    }
}
