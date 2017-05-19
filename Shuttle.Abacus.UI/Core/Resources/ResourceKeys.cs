using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Resources
{
    public class ResourceKeys
    {
        public static readonly ResourceKey Formula = new ResourceKey("resource://abacus/formula", Permissions.Formula);
        public static readonly ResourceKey FormulaOperation = new ResourceKey("resource://abacus/formula-operation", Permissions.Formula);
        public static readonly ResourceKey FormulaConstraint = new ResourceKey("resource://abacus/formula-constraint", Permissions.Formula);
        public static readonly ResourceKey Test = new ResourceKey("resource://abacus/test", Permissions.Test);
        public static readonly ResourceKey TestArgumentValue = new ResourceKey("resource://abacus/test-argument-value", Permissions.Test);
        public static readonly ResourceKey Matrix = new ResourceKey("resource://abacus/matrix", Permissions.Matrix);
        public static readonly ResourceKey Argument = new ResourceKey("resource://abacus/argument", Permissions.Argument);
        public static readonly ResourceKey ArgumentValue = new ResourceKey("resource://abacus/argument-value", Permissions.Argument);
    }
}
