using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IArgumentService
    {
        IMethodContext MethodContext(string name, IEnumerable<KeyValuePair<string, string>> answers);
    }
}
