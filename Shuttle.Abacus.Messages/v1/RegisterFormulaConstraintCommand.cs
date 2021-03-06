﻿using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterFormulaConstraintCommand
    {
        public Guid FormulaId { get; set; }
        public Guid Id { get; set; }
        public Guid ArgumentId { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}