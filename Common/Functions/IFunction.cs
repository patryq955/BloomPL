namespace Common.HashFunctions
{
    public interface IFunction
    {
        long Hash(long number, long modulo);
    }
}
