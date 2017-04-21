using System;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Application
{
    public abstract class ApplicationService
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
        public ITaskFactory TaskFactory { get; set; }

        protected IResult ScopedResult(Func<IResult> func)
        {
            Guard.AgainstNullDependency(UnitOfWorkProvider, "UnitOfWorkProvider");

            try
            {
                using (UnitOfWorkProvider.Create())
                {
                    return func.Invoke();
                }
            }
            catch (Exception ex)
            {
                return Result.Create().AddException(ex);
            }
        }

        protected IResult<TValue> ScopedResult<TValue>(Func<IResult<TValue>> func)
        {
            Guard.AgainstNullDependency(UnitOfWorkProvider, "UnitOfWorkProvider");

            try
            {
                using (UnitOfWorkProvider.Create())
                {
                    return func.Invoke();
                }
            }
            catch (Exception ex)
            {
                return Result<TValue>.Create().AddException(ex);
            }
        }

        protected IResult TransactedResult(Func<IResult> func)
        {
            Guard.AgainstNullDependency(UnitOfWorkProvider, "UnitOfWorkProvider");

            try
            {
                using (var uow = UnitOfWorkProvider.Create().Begin())
                {
                    var result = func.Invoke();

                    if (result.OK)
                    {
                        uow.Commit();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                return Result.Create().AddException(ex);
            }
        }

        protected IResult<TValue> TransactedResult<TValue>(Func<IResult<TValue>> func)
        {
            Guard.AgainstNullDependency(UnitOfWorkProvider, "UnitOfWorkProvider");

            try
            {
                using (var uow = UnitOfWorkProvider.Create().Begin())
                {
                    var result = func.Invoke();

                    if (result.OK)
                    {
                        uow.Commit();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                return Result<TValue>.Create().AddException(ex);
            }
        }
    }
}
