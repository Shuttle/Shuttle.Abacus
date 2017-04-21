using System;
using System.Collections.Generic;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class CalculationRepository : Repository<Calculation>, ICalculationRepository
    {
        private readonly IDataRowRepository<Calculation> calculationRepository;
        private readonly IDatabaseGateway gateway;

        public CalculationRepository(IDataRowRepository<Calculation> dataRowMapper, IDatabaseGateway gateway)
        {
            this.calculationRepository = dataRowMapper;
            this.gateway = gateway;
        }

        public override void Add(Calculation item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Calculation item)
        {
            gateway.ExecuteUsing(CalculationTableAccess.Remove(item));
        }

        public override Calculation Get(Guid id)
        {
            var result = calculationRepository.FetchItemUsing(CalculationTableAccess.Get(id));

            Guard.AgainstMissing<Calculation>(result, id);

            return result;
        }

        public void SaveOrdered(Method method)
        {
            var sequence = 1;

            method.CalculationCollection.Flattened().ForEach(calculation =>
                {
                    gateway.ExecuteUsing(CalculationTableAccess.SetSequenceNumber(calculation.Id, sequence));

                    sequence++;
                });
        }

        public void Add(Method method, ICalculationOwner owner, Calculation entity)
        {
            gateway.ExecuteUsing(CalculationTableAccess.Add(method, owner, entity));

            AddGraphNodeArguments(entity);
        }

        public void Save(Calculation item)
        {
            gateway.ExecuteUsing(CalculationTableAccess.Save(item));

            gateway.ExecuteUsing(GraphNodeArgumentTableAccess.RemoveFor(item.Id));

            AddGraphNodeArguments(item);
        }

        private void AddGraphNodeArguments(Calculation calculation)
        {
            var sequence = 1;

            calculation.GraphNodeArguments.ForEach(item =>
                {
                    gateway.ExecuteUsing(GraphNodeArgumentTableAccess.Add(calculation, item, sequence));

                    sequence++;
                });
        }

        public IEnumerable<Calculation> AllForOwner(Guid ownerId)
        {
            return calculationRepository.FetchAllUsing(CalculationTableAccess.AllForOwner(ownerId));
        }

        public void SaveOwnershipGraph(Method method)
        {
            var sequence = 1;

            SaveOwnershipGraph(method.CalculationCollection, typeof(Method).Name, method.Id, ref sequence);
        }

        private void SaveOwnershipGraph(IEnumerable<Calculation> calculations, string ownerName, Guid ownerId, ref int sequence)
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
            gateway.ExecuteUsing(CalculationTableAccess.Save(calculation, ownerName, ownerId, sequence));
        }
    }
}
