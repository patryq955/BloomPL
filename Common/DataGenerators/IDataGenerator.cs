using System.Collections.Generic;

namespace Common.DataGenerators
{
    public interface IDataGenerator
    {
        IEnumerable<int> Generate();
    }
}
