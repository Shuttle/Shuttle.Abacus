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

using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class PermissionCollectionTest 
    {
        [Test]
        public void Should_be_able_to_verify_access_using_the_permission_collection()
        {
            var permissions = CreateSUT();

            Assert.IsTrue(permissions.HasAccessToAnyOf("action-bogus", "action-1", "another-bogus"));
            Assert.IsFalse(permissions.HasAccessToAnyOf("action-bogus1", "action-bogus2", "action-bogus3"));

            Assert.IsTrue(permissions.HasAccessToAllOf("action-2", "action-4"));
            Assert.IsFalse(permissions.HasAccessToAllOf("action-2", "action-4", "action-bogus"));

            Assert.IsTrue(permissions.HasAccessTo("action-3"));
            Assert.IsFalse(permissions.HasAccessTo("action-false"));
        }

        [Test]
        public void Should_be_able_to_remove_one_collection_from_another()
        {
            var permissions = CreateSUT().Remove(new PermissionCollection { new Permission("action-2"), new Permission("action-4") });

            Assert.IsTrue(permissions.HasAccessToAllOf("action-1", "action-3", "action-5"));
            Assert.IsFalse(permissions.HasAccessToAnyOf("action-2", "action-4"));
        }


        private static PermissionCollection CreateSUT()
        {
            return new PermissionCollection
                       {
                           new Permission("action-1"),
                           new Permission("action-2"),
                           new Permission("action-3"),
                           new Permission("action-4"),
                           new Permission("action-5")
                       };
        }
    }
}
