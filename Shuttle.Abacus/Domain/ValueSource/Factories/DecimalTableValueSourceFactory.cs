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

        public string Name => "Matrix";

        public string Text => "Decimal Table";

        public string Type => "Selection";

        public IValueSource Create(string value)
        {
            return new DecimalTableValueSource(_matrixRepository.Get(new Guid(value)));
        }
    }
}
