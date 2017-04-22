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

        public string Name
        {
            get { return "ArgumentAnswer"; }
        }

        public string Text
        {
            get { return "Argument Answer"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new ArgumentAnswerValueSource(_argumentRepository.Get(new Guid(value)));
        }
    }
}
