namespace Common.HashFunctions
{
    public sealed class ExtendedFunction : IFunction
    {
        private readonly long _a;
        private readonly long _b;
        private readonly long _prime;

        public ExtendedFunction(long a, long b, long prime)
        {
            _a = a;
            _b = b;
            _prime = prime;
        }

        public long Hash(long number, long modulo) => ((_a * number + _b) % _prime) % modulo;

        public override string ToString() => "Rozrzerzona funkcja mieszająca";
    }
}