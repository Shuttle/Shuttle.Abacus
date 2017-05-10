using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class FormulaCommand
    {
        public FormulaCommand()
        {
            Commands = new List<object>();
        }

        public Guid FormulaId { get; set; }

        public List<object> Commands { get; set; }
    }
}