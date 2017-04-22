using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Resources
{
    public class ResourceKeys
    {
        public static readonly ResourceKey Calculation = new ResourceKey("resource://abacus/calculation", Permissions.Method);
        public static readonly ResourceKey Constraint = new ResourceKey("resource://abacus/constraint", Permissions.Method);
        public static readonly ResourceKey Limit = new ResourceKey("resource://abacus/limit", Permissions.Method);
        public static readonly ResourceKey MethodTest = new ResourceKey("resource://abacus/methodtest", Permissions.MethodTest);
        public static readonly ResourceKey Formula = new ResourceKey("resource://abacus/formula", Permissions.Method);
        public static readonly ResourceKey DecimalTable = new ResourceKey("resource://abacus/decimaltable", Permissions.DecimalTable);
        public static readonly ResourceKey Method = new ResourceKey("resource://abacus/method", Permissions.Method);
        public static readonly ResourceKey Argument = new ResourceKey("resource://abacus/argument", Permissions.Argument);
    }
}
