using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationRepository : Repository<Calculation>, ICalculationRepository
    {
        private readonly ICalculationQueryFactory _calculationQueryFactory;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataRepository<Calculation> _repository;

        public CalculationRepository(IDatabaseGateway databaseGateway, ICalculationQueryFactory calculationQueryFactory,
            IDataRepository<Calculation> dataRowMapper)
        {
            _repository = dataRowMapper;
            _databaseGateway = databaseGateway;
            _calculationQueryFactory = calculationQueryFactory;
        }

        public override void Add(Calculation item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Calculation item)
        {
            _databaseGateway.ExecuteUsing(_calculationQueryFactory.Remove(item));
        }

        public override Calculation Get(Guid id)
        {
            var result = _repository.FetchItemUsing(_calculationQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity<Calculation>(id);
            }

            return result;
        }

        public void SaveOrdered(Method method)
        {
            var sequence = 1;

            method.CalculationCollection.Flattened().ForEach(calculation =>
            {
                _databaseGateway.ExecuteUsing(_calculationQueryFactory.SetSequenceNumber(calculation.Id, sequence));

                sequence++;
            });
        }

        public void Add(Method method, ICalculationOwner owner, Calculation entity)
        {
            _databaseGateway.ExecuteUsing(_calculationQueryFactory.Add(method, owner, entity));

            AddGraphNodeArguments(entity);
        }

        public void Save(Calculation item)
        {
            _databaseGateway.ExecuteUsing(_calculationQueryFactory.Save(item));

            _databaseGateway.ExecuteUsing(GraphNodeArgumentQueryFactory.RemoveFor(item.Id));

            AddGraphNodeArguments(item);
        }

        public IEnumerable<Calculation> AllForOwner(Guid ownerId)
        {
            return _repository.FetchAllUsing(_calculationQueryFactory.AllForOwner(ownerId));
        }

        public void SaveOwnershipGraph(Method method)
        {
            var sequence = 1;

            SaveOwnershipGraph(method.CalculationCollection, typeof(Method).Name, method.Id, ref sequence);
        }

        private void AddGraphNodeArguments(Calculation calculation)
        {
            var sequence = 1;

            calculation.GraphNodeArguments.ForEach(item =>
            {
                _databaseGateway.ExecuteUsing(GraphNodeArgumentQueryFactory.Add(calculation, item, sequence));

                sequence++;
            });
        }

        private void SaveOwnershipGraph(IEnumerable<Calculation> calculations, string ownerName, Guid ownerId,
            ref int sequence)
        {
            foreach (var calculation in calculations)
            {
                var collection = calculation as CalculationCollection;

                if (collection != null)
                {
                    SaveOwnershipGraph(collection, typeof(Calculation).Name, calculation.Id, ref sequence);
                }

                Save(calculation, ownerName, ownerId, ref sequence);

                sequence++;
            }
        }

        private void Save(Calculation calculation, string ownerName, Guid ownerId, ref int sequence)
        {
            _databaseGateway.ExecuteUsing(_calculationQueryFactory.Save(calculation, ownerName, ownerId, sequence));
        }
    }
}