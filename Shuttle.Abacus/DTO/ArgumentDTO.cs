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

using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class ArgumentDTO
    {
        private static readonly List<string> Numbers = new List<string>
                                                       {
                                                           "decimal", "integer", "money"
                                                       };

        public ArgumentDTO()
        {
            Answers = new List<ArgumentRestrictedAnswerDTO>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }

        public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }

        public bool IsNumber
        {
            get { return !string.IsNullOrEmpty(AnswerType) && Numbers.Contains(AnswerType.ToLower()); }
        }

        public bool HasAnswerCatalog
        {
            get { return Answers != null && Answers.Count > 0; }
        }

        public bool IsMoney
        {
            get { return AnswerType.ToLower() == "money"; }
        }

        public bool IsText
        {
            get { return AnswerType.ToLower() == "text"; }
        }

        public bool CanOnlyCompareEquality
        {
            get { return HasAnswerCatalog || IsText; }
        }

        public bool HasArgumentName(string name)
        {
            return Answers.Find(item => item.Answer.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null;
        }
    }
}
