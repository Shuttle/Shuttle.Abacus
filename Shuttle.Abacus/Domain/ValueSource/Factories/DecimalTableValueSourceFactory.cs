using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableValueSourceFactory : IValueSourceFactory
    {
        private readonly IMatrixRepository _matrixRepository;

        public DecimalTableValueSourceFactory(IMatrixRepository matrixRepository)
        {
            _matrixRepository = matrixRepository;
        }

        public string Name
        {
            get { return "Matrix"; }
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
            return new DecimalTableValueSource(_matrixRepository.Get(new Guid(value)));
        }
    }
}
