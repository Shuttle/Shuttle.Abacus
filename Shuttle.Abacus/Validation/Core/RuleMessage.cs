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
using System.Collections.ObjectModel;

namespace Shuttle.Abacus
{
    public class RuleMessage
    {
        private readonly List<string> detailMessages = new List<string>();

        public RuleMessage(string message)
        {
            Text = message;
        }

        public string Text { get; internal set; }

        public IEnumerable<string> DetailMessages
        {
            get { return new ReadOnlyCollection<string>(detailMessages); }
        }

        public void AddDetailMessage(string message)
        {
            detailMessages.Add(message);
        }

        public void AddDetailMessages(IEnumerable<string> messages)
        {
            detailMessages.AddRange(messages);
        }

        public void AddDetailMessages(IEnumerable<RuleMessage> messages)
        {
            foreach (var message in messages)
            {
                detailMessages.Add(message.Text);
            }
        }
    }
}
