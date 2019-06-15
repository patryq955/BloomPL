namespace Common.HashFunctions
{
    public sealed class BaseFunction : IFunction
    {
        public long Hash(long number, long modulo) => number % modulo;

        public override string ToString() => "Prosta funkcja mieszająca";
    }
}