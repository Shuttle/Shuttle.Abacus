using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentAnswerDTOMapper : IMapper<ArgumentAnswerDTO, ArgumentAnswer>
    {
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;

        public ArgumentAnswerDTOMapper(IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider)
        {
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public ArgumentAnswer MapFrom(ArgumentAnswerDTO input)
        {
            return argumentAnswerFactoryProvider
                .Get(input.AnswerType)
                .Create(input.ArgumentName, input.Answer);
        }
    }
}