namespace Shuttle.Abacus.Infrastructure
{
    public static class Permissions
    {
        public static readonly IPermission Null = new NullPermission();

        public static readonly IPermission Formula = new Permission("permission://abacus/formula", "Formula Management");
        public static readonly IPermission DecimalTable = new Permission("permission://abacus/decimaltable", "Decimal Table Management");
        public static readonly IPermission SystemUser = new Permission("permission://abacus/systemuser", "System User Management");
        public static readonly IPermission MethodTest = new Permission("permission://abacus/methodtest", "Formula Test Management");
        public static readonly IPermission Argument = new Permission("permission://abacus/argument", "Argument Management");

        public static IPermissionCollection All()
        {
            return new PermissionCollection
                   {
                       Formula, 
                       Argument,
                       DecimalTable, 
                       SystemUser, 
                       MethodTest
                   };
        }
    }
}
