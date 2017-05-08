namespace Shuttle.Abacus.Infrastructure
{
    public static class Permissions
    {
        public static readonly IPermission Null = new NullPermission();

        public static readonly IPermission Formula = new Permission("permission://abacus/formula", "Formula Management");
        public static readonly IPermission Matrix = new Permission("permission://abacus/matrix", "Matrix Management");
        public static readonly IPermission SystemUser = new Permission("permission://abacus/systemuser", "System User Management");
        public static readonly IPermission Test = new Permission("permission://abacus/test", "Formula Test Management");
        public static readonly IPermission Argument = new Permission("permission://abacus/argument", "Argument Management");

        public static IPermissionCollection All()
        {
            return new PermissionCollection
                   {
                       Formula, 
                       Argument,
                       Matrix, 
                       SystemUser, 
                       Test
                   };
        }
    }
}
