using System;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestRepository : Repository<MethodTest>, IMethodTestRepository
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataTableRepository<MethodTest> repository;

        public MethodTestRepository(IDataTableRepository<MethodTest> repository, IDatabaseGateway gateway)
        {
            this.repository = repository;
            this.gateway = gateway;
        }

        public override void Add(MethodTest item)
        {
            gateway.ExecuteUsing(MethodTestTable.Add(item));

            AddArgumentAnswers(item);
        }

        private void AddArgumentAnswers(MethodTest test)
        {
            test.ArgumentAnswers.ForEach(entry => gateway.ExecuteUsing(MethodTestTable.AddArgumentAnswer(test, entry)));
        }

        public override void Remove(MethodTest item)
        {
            gateway.ExecuteUsing(MethodTestTable.Remove(item));
        }

        public override MethodTest Get(Guid id)
        {
            return repository.FetchItemUsing(MethodTestTable.Get(id));
        }

        public void Save(MethodTest item)
        {
            gateway.ExecuteUsing(MethodTestTable.Remove(item));
            gateway.ExecuteUsing(MethodTestTable.Add(item));

            AddArgumentAnswers(item);
        }

        public void SetArgumentName(Guid argumentId, string argumentName)
        {
            gateway.ExecuteUsing(MethodTestTable.SetArgumentName(argumentId, argumentName));
        }

        public void SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            gateway.ExecuteUsing(MethodTestTable.SetArgumentAnswerType(argumentId, answerType));
        }
    }
}
