namespace Common.HashFunctions
{
    public sealed class SimpleHashFunction : IHashFunction
    {
        public long Calculate(long number, long modulo) => number % modulo;

        public override string ToString() => "Prosta funkcja mieszająca";
    }
}