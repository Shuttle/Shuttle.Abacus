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

namespace Shuttle.Abacus
{
    public static class Permissions
    {
        public static readonly IPermission Null = new NullPermission();

        public static readonly IPermission Method = new Permission("permission://abacus/method", "Method Management");
        public static readonly IPermission DecimalTable = new Permission("permission://abacus/decimaltable", "Decimal Table Management");
        public static readonly IPermission SystemUser = new Permission("permission://abacus/systemuser", "System User Management");
        public static readonly IPermission MethodTest = new Permission("permission://abacus/methodtest", "Method Test Management");
        public static readonly IPermission Argument = new Permission("permission://abacus/argument", "Argument Management");

        public static IPermissionCollection All()
        {
            return new PermissionCollection
                   {
                       Method, 
                       Argument,
                       DecimalTable, 
                       SystemUser, 
                       MethodTest
                   };
        }
    }
}
