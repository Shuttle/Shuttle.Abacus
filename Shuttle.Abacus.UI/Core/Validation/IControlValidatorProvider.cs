using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IControlValidatorProvider
    {
        IControlValidatorProvider Register(IControlValidator validator);
        IEnumerable<IControlValidator> Validators { get; }
        IControlValidator GetFor<T>();
        IControlValidator GetFor(Type type);
    }
}
