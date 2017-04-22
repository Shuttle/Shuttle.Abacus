using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Tests
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
