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

namespace Shuttle.Abacus
{
    public class SystemUser
    {
        public SystemUser()
        {
            Construct();
        }

        public SystemUser(Guid id)
        {
            Construct();
        }

        public SystemUser(ICreateSystemUserCommand command)
        {
            Construct();

            LoginName = command.LoginName;

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));
        }

        public IPermissionCollection Permissions { get; set; }

        public string LoginName { get; set; }

        private void Construct()
        {
            Permissions = new PermissionCollection();
        }

        public SystemUser ProcessCommand(ISetPermissionsCommand command)
        {
            Permissions = new PermissionCollection();

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));

            return this;
        }

        public SystemUser ProcessCommand(IChangeLoginNameCommand command)
        {
            LoginName = command.NewLoginName;

            return this;
        }
    }
}
