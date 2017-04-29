using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentRepository : Repository<Argument>, IArgumentRepository
    {
        private readonly IArgumentQueryFactory _argumentQueryFactory;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataRepository<Argument> _repository;

        public ArgumentRepository(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory,
            IDataRepository<Argument> repository)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(argumentQueryFactory, "argumentQueryFactory");
            Guard.AgainstNull(repository, "repository");

            _repository = repository;
            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
        }

        public override void Add(Argument item)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Add(item));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_argumentQueryFactory.Remove(id));
        }

        public override Argument Get(Guid id)
        {
            return Guarded.Entity(Find(id), id);
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

        public Argument Find(Guid id)
        {
            return _repository.FetchItemUsing(_argumentQueryFactory.Get(id));
        }
    }
}