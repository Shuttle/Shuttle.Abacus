using System;

namespace Shuttle.Abacus.Domain
{
    public interface ITestRepository
    {
        Test Get(Guid id);
    }
}