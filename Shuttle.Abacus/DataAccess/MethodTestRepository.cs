using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestRepository : Repository<MethodTest>, IMethodTestRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMethodTestQueryFactory _methodTestQueryFactory;
        private readonly IDataTableRepository<MethodTest> _repository;

        public MethodTestRepository(IDatabaseGateway databaseGateway, IMethodTestQueryFactory methodTestQueryFactory,
            IDataTableRepository<MethodTest> repository)
        {
            _repository = repository;
            _databaseGateway = databaseGateway;
            _methodTestQueryFactory = methodTestQueryFactory;
        }

        public override void Add(MethodTest item)
        {
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.Add(item));

            AddArgumentAnswers(item);
        }

        public override void Remove(MethodTest item)
        {
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.Remove(item));
        }

        public override MethodTest Get(Guid id)
        {
            return _repository.FetchItemUsing(_methodTestQueryFactory.Get(id));
        }

        public void Save(MethodTest item)
        {
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.Remove(item));
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.Add(item));

            AddArgumentAnswers(item);
        }

        public void SetArgumentName(Guid argumentId, string argumentName)
        {
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.SetArgumentName(argumentId, argumentName));
        }

        public void SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            _databaseGateway.ExecuteUsing(_methodTestQueryFactory.SetArgumentAnswerType(argumentId, answerType));
        }

        private void AddArgumentAnswers(MethodTest test)
        {
            test.ArgumentAnswers.ForEach(
                entry => _databaseGateway.ExecuteUsing(_methodTestQueryFactory.AddArgumentAnswer(test, entry)));
        }
    }
}