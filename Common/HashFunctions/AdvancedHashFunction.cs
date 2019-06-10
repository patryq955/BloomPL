namespace Common.HashFunctions
{
    public sealed class AdvancedHashFunction : IHashFunction
    {
        private readonly long _a;
        private readonly long _b;
        private readonly long _prime;

        public AdvancedHashFunction(long a, long b, long prime)
        {
            _a = a;
            _b = b;
            _prime = prime;
        }

        public long Calculate(long number, long modulo) => ((_a * number + _b) % _prime) % modulo;

        public override string ToString() => "Zaawansowana funkcja mieszająca";
    }
}