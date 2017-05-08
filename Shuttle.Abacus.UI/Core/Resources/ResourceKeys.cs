using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Resources
{
    public class ResourceKeys
    {
        public static readonly ResourceKey Constraint = new ResourceKey("resource://abacus/constraint", Permissions.Formula);
        public static readonly ResourceKey Test = new ResourceKey("resource://abacus/methodtest", Permissions.Test);
        public static readonly ResourceKey Formula = new ResourceKey("resource://abacus/formula", Permissions.Formula);
        public static readonly ResourceKey Matrix = new ResourceKey("resource://abacus/matrix", Permissions.Matrix);
        public static readonly ResourceKey Argument = new ResourceKey("resource://abacus/argument", Permissions.Argument);
    }
}
