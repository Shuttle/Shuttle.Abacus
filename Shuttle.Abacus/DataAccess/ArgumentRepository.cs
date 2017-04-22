using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentRepository : Repository<Argument>, IArgumentRepository
    {
        private readonly ICache _cache;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IArgumentQueryFactory _argumentQueryFactory;
        private readonly IDataRepository<Argument> _repository;

        public ArgumentRepository(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory, IDataRepository<Argument> repository, ICache cache)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
            _cache = cache;
        }

        public override void Add(Argument item)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Add(item));
        }

        public override void Remove(Argument item)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Remove(item));
        }

        public override Argument Get(Guid id)
        {
            var key = string.Format("Argument|{0}", id);

            var result = _cache.Get<Argument>(key);

            if (result != null)
            {
                return result;
            }

            result = _repository.FetchItemUsing(_argumentQueryFactory.Get(id));

            Guard.AgainstMissing<Argument>(result, id);

            _cache.Add(key, result);

            return result;
        }

        public ArgumentCollection All()
        {
            return new ArgumentCollection(_repository.FetchAllUsing(_argumentQueryFactory.All()));
        }

        public void Save(Argument argument)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Save(argument));

            _databaseGateway.ExecuteUsing(_argumentQueryFactory.RemoveRestrictedAnswers(argument));

            argument.RestrictedAnswers.ForEach(
                mapping => _databaseGateway.ExecuteUsing(_argumentQueryFactory.SaveRestrictedAnswers(argument, mapping)));
        }
    }
}