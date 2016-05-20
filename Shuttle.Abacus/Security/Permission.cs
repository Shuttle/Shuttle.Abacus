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
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class Permission : IPermission
    {
        public Permission(string identifier)
            : this(identifier, identifier)
        {
        }

        public Permission(string identifier, string description)
        {
            Identifier = identifier;
            Description = description;
        }

        public string Identifier { get; private set; }
        public string Description { get; set; }

        public bool IsSatisfiedBy(IPermissionCollection permissions)
        {
            Guard.AgainstNull(permissions, "permissions");

            return permissions.HasAccessTo(Identifier);
        }

        public override string ToString()
        {
            return Identifier;
        }

        public static implicit operator string(Permission permission)
        {
            return permission.Identifier;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as IPermission;

            return (other == null
                        ? Convert.ToString(obj)
                        : other.Identifier).Equals(Identifier,
                                                   StringComparison.
                                                       InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Identifier.ToLower().GetHashCode();
        }
    }
}
