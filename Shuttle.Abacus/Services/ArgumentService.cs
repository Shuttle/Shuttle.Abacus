/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
