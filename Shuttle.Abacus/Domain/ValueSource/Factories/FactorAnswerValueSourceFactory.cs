using System;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentAnswerValueSourceFactory : IValueSourceFactory
    {
        private readonly IArgumentRepository _argumentRepository;

        public ArgumentAnswerValueSourceFactory(IArgumentRepository argumentRepository)
        {
            _argumentRepository = argumentRepository;
        }

        public string Name => "ValueType";

        public string Text => "Argument Answer";

        public string Type => "Selection";

        public IValueSource Create(string value)
        {
            return new ArgumentAnswerValueSource(_argumentRepository.Get(new Guid(value)));
        }
    }
}
