using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;
using IPipeline = Shuttle.Abacus.Infrastructure.IPipeline;

namespace Shuttle.Abacus.Domain
{
    public class ArgumentService : IArgumentService
    {
        private readonly IArgumentAnswerFactory argumentAnswerFactory;
        private readonly IPipeline pipeline;
        private readonly IArgumentRepository argumentRepository;

        public ArgumentService(IArgumentRepository argumentRepository,
                             IArgumentAnswerFactory argumentAnswerFactory,
                             IPipeline pipeline)
        {
            this.argumentRepository = argumentRepository;
            this.argumentAnswerFactory = argumentAnswerFactory;
            this.pipeline = pipeline;
        }

        public IMethodContext MethodContext(string name, IEnumerable<KeyValuePair<string, string>> answers)
        {
            Guard.AgainstNull(answers, "answers");

            var arguments = argumentRepository.All();

            IMethodContext result = new MethodContext(name);

            foreach (var pair in answers)
            {
                var answer = pair.Value;
                if (!string.IsNullOrEmpty(answer))
                {
                    var argument = arguments.Find(pair.Key);

                    if (argument == null)
                    {
                        result.AddWarningMessage(string.Format("Could not find an argument with name '{0}'.", pair.Key));
                    }
                    else
                    {
                        if (argument.HasRestrictedAnswers && !argument.ContainsAnswer(answer))
                        {
                            result.AddWarningMessage(string.Format("Answer '{0}' is not valid for argument '{1}'.", answer, argument.Name));
                        }

                        result.AddArgumentAnswer(argumentAnswerFactory.Create(argument.AnswerType, argument.Name, answer));
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

            pipeline.Process(result);

            return result;
        }
    }
}
