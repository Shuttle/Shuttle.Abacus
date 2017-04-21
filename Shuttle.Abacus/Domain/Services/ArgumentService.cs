using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ArgumentService : IArgumentService
    {
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;
        private readonly IPipeline pipeline;
        private readonly IArgumentRepository argumentRepository;

        public ArgumentService(IArgumentRepository argumentRepository,
                             IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
                             IPipeline pipeline)
        {
            this.argumentRepository = argumentRepository;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
            this.pipeline = pipeline;
        }

        public IMethodContext MethodContext(string name, IEnumerable<KeyValuePair<string, string>> answers)
        {
            Guard.AgainstNull(answers, "answers");

            var arguments = argumentRepository.All();

            IMethodContext result = new MethodContext(name);

            foreach (var pair in answers)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                {
                    var argument = arguments.Find(pair.Key);

                    if (argument == null)
                    {
                        result.AddWarningMessage(string.Format("Could not find an argument with name '{0}'.", pair.Key));
                    }
                    else
                    {
                        if (argument.HasRestrictedAnswers)
                        {
                            var answer = argument.FindAnswer(pair.Value);

                            if (answer == null)
                            {
                                result.AddWarningMessage(string.Format("Answer '{0}' is not valid for argument '{1}'.", pair.Value, argument.Name));
                            }
                            else
                            {
                                result.AddArgumentAnswer(argumentAnswerFactoryProvider.Get(argument.AnswerType).Create(argument.Name, answer.Answer));
                            }
                        }
                        else
                        {
                            result.AddArgumentAnswer(argumentAnswerFactoryProvider.Get(argument.AnswerType).Create(argument.Name, pair.Value));
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(pair.Key))
                    {
                        result.AddInformationMessage(string.Format("Argument '{0}' has no answer.", pair.Key));
                    }
                }
            }

            //TODO: pipeline.Process(result);

            return result;
        }
    }
}
