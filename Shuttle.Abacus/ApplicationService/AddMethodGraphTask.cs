using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.ApplicationService
{
    public class AddMethodGraphTask : IAddMethodGraphTask
    {
        private readonly ICalculationRepository calculationRepository;
        private readonly IConstraintRepository constraintRepository;
        private readonly IFormulaRepository formulaRepository;
        private readonly ILimitRepository limitRepository;
        private readonly IMethodRepository methodRepository;

        public AddMethodGraphTask(ICalculationRepository calculationRepository, IConstraintRepository constraintRepository, IFormulaRepository formulaRepository, ILimitRepository limitRepository, IMethodRepository methodRepository)
        {
            this.calculationRepository = calculationRepository;
            this.constraintRepository = constraintRepository;
            this.formulaRepository = formulaRepository;
            this.limitRepository = limitRepository;
            this.methodRepository = methodRepository;
        }

        public void Execute(Method method)
        {
            methodRepository.Add(method);

            SaveLimits(method);
            SaveCalculations(method, method);
        }

        private void SaveLimits(ILimitOwner limitOwner)
        {
            limitOwner.Limits.ForEach(limit =>
                {
                    limitRepository.Add(limitOwner, limit);

                    limit.Constraints.ForEach(constraint => constraintRepository.SaveForOwner(limit));

                    SaveFormulas(limit);
                });
        }

        private void SaveFormulas(IFormulaOwner formulaOwner)
        {
            formulaOwner.Formulas.ForEach(formula =>
                {
                    formulaRepository.Add(formulaOwner, formula);

                    SaveConstraints(formula);
                });

            formulaRepository.SaveOrdered(formulaOwner);
        }

        private void SaveConstraints(IConstraintOwner constraintOwner)
        {
            constraintOwner.Constraints.ForEach(constraint => constraintRepository.SaveForOwner(constraintOwner));
        }

        private void SaveCalculations(Method method, ICalculationOwner owner)
        {
            owner.Calculations.ForEach(calculation =>
                {
                    calculationRepository.Add(method, owner, calculation);

                    var calculationOwner = calculation as ICalculationOwner;

                    if (calculationOwner != null)
                    {
                        SaveCalculations(method, calculationOwner);
                    }

                    SaveLimits(calculation);

                    var formulaOwner = calculation as IFormulaOwner;

                    if (formulaOwner != null)
                    {
                        SaveFormulas(formulaOwner);
                    }
                });

            calculationRepository.SaveOrdered(method);
        }
    }
}
