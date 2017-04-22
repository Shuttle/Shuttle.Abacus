using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableValueSourceFactory : IValueSourceFactory
    {
        private readonly IDecimalTableRepository _decimalTableRepository;

        public DecimalTableValueSourceFactory(IDecimalTableRepository decimalTableRepository)
        {
            _decimalTableRepository = decimalTableRepository;
        }

        public string Name
        {
            get { return "DecimalTable"; }
        }

        public string Text
        {
            get { return "Decimal Table"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new DecimalTableValueSource(_decimalTableRepository.Get(new Guid(value)));
        }
    }
}
