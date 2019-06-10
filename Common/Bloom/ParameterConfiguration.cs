namespace Common.Bloom
{
    public interface IParameterConfiguration
    {
        int N { get; }
        int NumberHashFunctions { get; }
        int Factor { get; }
        int FilterSize { get; }
    }
}
