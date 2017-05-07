using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaContextRegister : IFormulaContextRegister
    {
        private readonly ICalculationLogger logger;
        private readonly Dictionary<string, ICalculationResult> results;
        private readonly Dictionary<string, SubTotalCalculationResult> subtotals;

        public FormulaContextRegister(string name, ICalculationLogger logger)
            : this(name, logger, new Dictionary<string, ICalculationResult>(), new Dictionary<string, SubTotalCalculationResult>())
        {
        }

        private FormulaContextRegister(string name, ICalculationLogger logger, Dictionary<string, ICalculationResult> results,
                                       Dictionary<string, SubTotalCalculationResult> subtotals)
        {
            this.logger = logger;
            this.results = results;
            this.subtotals = subtotals;

            throw new NotImplementedException();
            //Total = new FormulaCalculationResult(name, 0);
        }

        public IEnumerable<ICalculationResult> Results
        {
            get
            {
                var result = new List<ICalculationResult>();

                results.ForEach(entry => result.Add(entry.Value));

                return new ReadOnlyCollection<ICalculationResult>(result);
            }
        }

        public IEnumerable<ICalculationResult> SubTotals
        {
            get
            {
                var result = new List<ICalculationResult>();

                subtotals.ForEach(entry => result.Add(entry.Value));

                return new ReadOnlyCollection<ICalculationResult>(result);
            }
        }

        public ICalculationResult Total { get; private set; }

        public ICalculationResult GetResult(string name)
        {
            return results[name];
        }

        public bool HasResult(string name)
        {
            return results.ContainsKey(name);
        }

        public SubTotalCalculationResult GetSubTotal(string name)
        {
            return subtotals[name];
        }

        public void AddResult(ICalculationResult calculationResult)
        {
            Guard.AgainstNull(calculationResult, "calculationResult");

            try
            {
                results.Add(calculationResult.Name, calculationResult);
                subtotals.Add(calculationResult.Name, new SubTotalCalculationResult(calculationResult.Name, Total.Value));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("FormulaContextRegister.AddResult: {0} - {1}", calculationResult.Name, ex.Message));
            }

            if (logger.Enabled)
            {
                logger.AppendLine(calculationResult.Description());
            }
        }

        public void IncrementSubTotal(ICalculationResult calculationResult)
        {
            Guard.AgainstNull(calculationResult, "calculationResult");

            Total.Add(calculationResult);
        }
    }
}
