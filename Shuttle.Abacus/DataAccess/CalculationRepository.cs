using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationRepository : Repository<Calculation>, ICalculationRepository
    {
        private readonly ICalculationQuery _calculationQuery;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ICalculationQueryFactory _calculationQueryFactory;
        private readonly IFormulaQuery _formulaQuery;
        private readonly ILimitQuery _limitQuery;

        public CalculationRepository(IDatabaseGateway databaseGateway, ICalculationQueryFactory calculationQueryFactory,
            ICalculationQuery calculationQuery, IFormulaQuery formulaQuery, ILimitQuery limitQuery)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(calculationQueryFactory, "calculationQueryFactory");
            Guard.AgainstNull(calculationQuery, "calculationQuery");
            Guard.AgainstNull(formulaQuery, "formulaQuery");
            Guard.AgainstNull(limitQuery, "limitQuery");

            _databaseGateway = databaseGateway;
            _calculationQueryFactory = calculationQueryFactory;
            _calculationQuery = calculationQuery;
            _formulaQuery = formulaQuery;
            _limitQuery = limitQuery;
        }

        public override void Add(Calculation item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_calculationQueryFactory.Remove(id));
        }

        public override Calculation Get(Guid id)
        {
            var calculationRow = _calculationQuery.Get(id);

            if (calculationRow == null)
            {
                throw Exceptions.MissingEntity<Calculation>(id);
            }

            Calculation result;

            switch (CalculationColumns.Type.MapFrom(calculationRow).ToLower())
            {
                case "collection":
                {
                    result = new CalculationCollection(
                        id,
                        CalculationColumns.Name.MapFrom(calculationRow));

                    break;
                }
                default:
                {
                    result = new FormulaCalculation(
                        id,
                        CalculationColumns.Name.MapFrom(calculationRow),
                        CalculationColumns.Required.MapFrom(calculationRow));

                    break;
                }
            }

            var formulaOwner = result as IFormulaOwner;

            if (formulaOwner != null)
            {
                _formulaQuery.PopulateOwner(formulaOwner);
            }

            _limitQuery.PopulateOwner(result);

            var calculationOwner = result as ICalculationOwner;

            if (calculationOwner != null)
            {
                _calculationQuery.PopulateOwner(calculationOwner);
            }

            //TODO:
            //_graphNodeArgumentDataRowMapper.FetchAllUsing(GraphNodeArgumentQueryFactory.AllForCalculation(result))
            //    .ForEach(result.AddGraphNodeArgument);

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