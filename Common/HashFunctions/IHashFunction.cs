namespace Common.HashFunctions
{
    public interface IHashFunction
    {
        long Calculate(long number, long modulo);
    }
}
