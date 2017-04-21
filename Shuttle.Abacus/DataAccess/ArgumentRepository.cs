using System;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Validation;

namespace Abacus.Data
{
    public class ArgumentRepository : Repository<Argument>, IArgumentRepository
    {
        private readonly ICache cache;
        private readonly IDatabaseGateway gateway;
        private readonly IDataReaderRepository<Argument> repository;

        public ArgumentRepository(IDataReaderRepository<Argument> repository, IDatabaseGateway gateway, ICache cache)
        {
            this.repository = repository;
            this.gateway = gateway;
            this.cache = cache;
        }

        public override void Add(Argument item)
        {
            gateway.ExecuteUsing(ArgumentTableAccess.Add(item));
        }

        public override void Remove(Argument item)
        {
            gateway.ExecuteUsing(ArgumentTableAccess.Remove(item));
        }

        public override Argument Get(Guid id)
        {
            var key = string.Format("Argument|{0}", id);

            var result = cache.Get<Argument>(key);

            if (result != null)
            {
                return result;
            }

            result = repository.FetchItemUsing(ArgumentTableAccess.Get(id));

            Guard.AgainstMissing<Argument>(result, id);

            cache.Add(key, result);

            return result;
        }

        public ArgumentCollection All()
        {
            return new ArgumentCollection(repository.FetchAllUsing(ArgumentTableAccess.All()));
        }

        public void Save(Argument argument)
        {
            gateway.ExecuteUsing(ArgumentTableAccess.Save(argument));

            gateway.ExecuteUsing(ArgumentTableAccess.RemoveRestrictedAnswers(argument));

            argument.RestrictedAnswers.ForEach(mapping => gateway.ExecuteUsing(ArgumentTableAccess.SaveRestrictedAnswers(argument, mapping)));
        }
    }
}
