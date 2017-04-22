using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class MethodContext : IMethodContext
    {
        private readonly ArgumentAnswerCollection answers;
        private readonly ICalculationLogger logger;
        private readonly IMethodContextRegister register;

        public IGraphNode GraphNode { get; private set; }

        public MethodContext()
            : this(
                string.Empty, new NullCalculationLogger(), new ArgumentAnswerCollection(),
                new MethodContextRegister("register", new NullCalculationLogger()))
        {
        }

        public MethodContext(string name)
            : this(
                name, new NullCalculationLogger(), new ArgumentAnswerCollection(),
                new MethodContextRegister(name, new NullCalculationLogger()))
        {
        }

        public MethodContext(string name, ICalculationLogger logger)
            : this(name, logger, new ArgumentAnswerCollection(), new MethodContextRegister(name, logger))
        {
        }

        private MethodContext(string name, ICalculationLogger logger, ArgumentAnswerCollection answers,
                               IMethodContextRegister register)
        {
            Name = name;

            this.logger = logger;
            this.answers = answers;
            this.register = register;

            GraphNode = new GraphNode(name);
        }

        public string Name { get; private set; }

        public IMethodContext AssignGraphNode(IGraphNode item)
        {
            Guard.AgainstNull(item, "item");

            GraphNode = item;

            return this;
        }

        public void PopulateGraphNode(decimal total, decimal subTotal)
        {

        }

        public IEnumerable<ICalculationResult> Results
        {
            get { return register.Results; }
        }

        public IEnumerable<ICalculationResult> SubTotals
        {
            get { return register.SubTotals; }
        }

        public IEnumerable<ArgumentAnswer> ArgumentAnswers
        {
            get { return answers; }
        }

        public IEnumerable<string> ErrorMessages
        {
            get { return logger.ErrorMessages; }
        }

        public IEnumerable<string> WarningMessages
        {
            get { return logger.WarningMessages; }
        }

        public IEnumerable<string> InformationMessages
        {
            get { return logger.InformationMessages; }
        }

        public ICalculationResult Total
        {
            get { return register.Total; }
        }

        public bool LoggerEnabled
        {
            get { return logger.Enabled; }
        }

        public string LogText
        {
            get { return logger.ToString(); }
        }

        public bool OK
        {
            get { return !logger.HasErrorMessages; }
        }

        public List<GraphNodeDTO> GraphNodes()
        {
            var result = new List<GraphNodeDTO>();

            AddGraphNodeDTOs(result, GraphNode.GraphNodes);

            return result;
        }

        public void AddErrorMessage(string message)
        {
            logger.AddErrorMessage(message);
        }

        public void AddWarningMessage(string message)
        {
            logger.AddWarningMessage(message);
        }

        public void AddInformationMessage(string message)
        {
            logger.AddInformationMessage(message);
        }

        public ICalculationResult GetResult(string name)
        {
            if (!HasResult(name))
            {
                if (LoggerEnabled)
                {
                    Log(string.Format("Could not find requested calculation result '{0}'.  Returning 0.", name));
                }

                return new ZeroCalculationResult(name);
            }

            return register.GetResult(name);
        }

        public bool HasResult(string name)
        {
            return register.HasResult(name);
        }

        public SubTotalCalculationResult GetSubTotal(string name)
        {
            if (!HasResult(name))
            {
                if (LoggerEnabled)
                {
                    Log(string.Format("Could not find requested sub-total for calculation '{0}'.  Returning 0.", name));
                }

                return new SubTotalCalculationResult(name, 0);
            }

            return register.GetSubTotal(name);
        }

        public void Log(string text, params string[] args)
        {
            if (LoggerEnabled)
            {
                logger.AppendLine(text, args);
            }
        }

        public void IncreaseIndent()
        {
            logger.IncreaseIndent();
        }

        public void DecreaseIndent()
        {
            logger.DecreaseIndent();
        }

        public IMethodContext AddArgumentAnswer(ArgumentAnswer argumentAnswer)
        {
            if (answers.Contains(argumentAnswer.ArgumentName))
            {
                throw new DuplicateEntryException(
                    string.Format("There is already an argument answer with argument name '{0}' registered.",
                                  argumentAnswer.ArgumentName));
            }

            answers.Add(argumentAnswer);

            if (LoggerEnabled)
            {
                Log("Argument answer: {0} = {1}", argumentAnswer.ArgumentName, Convert.ToString(argumentAnswer.Answer));
            }

            return this;
        }

        public ArgumentAnswer GetArgumentAnswer(string argumentName)
        {
            if (!HasArgumentAnswer(argumentName))
            {
                AddWarningMessage(
                    string.Format("Could not find requested answer for argument with name '{0}'.  Using null answer.",
                                  argumentName));

                return ArgumentAnswer.Null;
            }

            return answers[argumentName];
        }

        public bool HasArgumentAnswer(string argumentName)
        {
            return answers.Contains(argumentName);
        }

        public void Log()
        {
            if (LoggerEnabled)
            {
                logger.AppendLine();
            }
        }

        public IMethodContext AsReadOnly()
        {
            ReadOnly = true;

            return this;
        }

        public bool ReadOnly { get; private set; }

        public IMethodContext AddResult(ICalculationResult calculationResult)
        {
            if (ReadOnly)
            {
                return this;
            }

            register.AddResult(calculationResult);

            return this;
        }

        public IMethodContext Wrapped(string title)
        {
            IncreaseIndent();

            return new MethodContext(title, logger, answers, new MethodContextRegister(title, logger));
        }

        public void Dispose()
        {
            DecreaseIndent();
        }

        public IMethodContext Copy()
        {
            return new MethodContext(Name, logger, answers, register);
        }

        public void LogResults()
        {
            if (!logger.Enabled)
            {
                return;
            }

            logger.AppendLine("Results:");

            foreach (var result in register.Results)
            {
                logger.AppendLine(result.Description());
            }
        }

        public void LogSubTotals()
        {
            if (!logger.Enabled)
            {
                return;
            }

            logger.AppendLine("Sub-totals:");

            foreach (var subtotal in register.SubTotals)
            {
                logger.AppendLine(subtotal.Description());
            }
        }

        public void IncrementSubTotal(ICalculationResult calculationResult)
        {
            if (ReadOnly)
            {
                return;
            }

            register.IncrementSubTotal(calculationResult);
        }

        public void AddGraphNode(ICalculationResult result, SubTotalCalculationResult subTotalCalculationResult)
        {
        }

        public MethodContextDTO DTO()
        {
            var result = new MethodContextDTO
                         {
                             Total = Total.Value
                         };

            AddGraphNodeDTOs(result.GraphNodes, GraphNode.GraphNodes);

            result.SetLog(LogText);

            result.ErrorMessages = new List<string>(logger.ErrorMessages);
            result.WarningMessages = new List<string>(logger.WarningMessages);
            result.InformationMessages = new List<string>(logger.InformationMessages);

            return result;
        }

        private void AddGraphNodeDTOs(ICollection<GraphNodeDTO> to, GraphNodeCollection from)
        {
            foreach (var item in from)
            {
                var dto = new GraphNodeDTO
                {
                    Name = item.Name,
                    Total = item.Total,
                    SubTotal = item.SubTotal
                };

                foreach (var argument in item.GraphNodeArguments)
                {
                    dto.AddGraphNodeArgument(argument.Argument.Name, argument.DisplayString(this));
                }

                if (item.GraphNodes.Any())
                {
                    AddGraphNodeDTOs(dto.GraphNodes, item.GraphNodes);
                }

                to.Add(dto);
            }
        }
    }
}
