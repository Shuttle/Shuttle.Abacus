﻿namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentSearchSpecification
    {
        public string Name { get; private set; }

        public ArgumentSearchSpecification MatchingName(string name)
        {
            Name = name;

            return this;
        }
    }
}