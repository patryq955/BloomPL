using System.Collections.Generic;

namespace Common.Creators
{
    public interface ICreator
    {
        IEnumerable<int> Create();
    }
}
