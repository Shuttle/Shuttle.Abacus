using System;

namespace Shuttle.Abacus
{
    public interface ITestRepository
    {
        Test Get(Guid id);
    }
}