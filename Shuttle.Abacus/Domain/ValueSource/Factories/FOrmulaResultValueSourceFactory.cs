using System;

namespace Shuttle.Abacus.Domain
{
    public class FormulaResultValueSourceFactory : IValueSourceFactory
    {
        public string Name => "RunningTotal";

        public string Text => "Calculation Result";

        public string Type => "Selection";

        public IValueSource Create(string value)
        {
            throw new NotImplementedException();
            //return new FormulaResultValueSource(_formulaRepository.Get(new Guid(value)));
        }
    }
}
