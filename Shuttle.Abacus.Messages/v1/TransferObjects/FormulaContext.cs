using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class FormulaContext
    {
        public List<ArgumentValue> ArgumentAnswers { get; set; }
        public List<FormulaContext> FormulaContexts { get; set; }

        public decimal Result { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateCompleted { get; set; }

        public FormulaContext()
        {
            ArgumentAnswers = new List<ArgumentValue>();
            FormulaContexts = new List<FormulaContext>();
        }
    }
}