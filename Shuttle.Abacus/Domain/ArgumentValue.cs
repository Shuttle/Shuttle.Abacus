﻿namespace Shuttle.Abacus
{
    public class ArgumentValue
    {
        public ArgumentValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}