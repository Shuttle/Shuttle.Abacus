using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IArgumentService
    {
        IMethodContext MethodContext(string name, IEnumerable<KeyValuePair<string, string>> answers);
    }
}
