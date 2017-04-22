using System;

namespace Shuttle.Abacus.UI.Core.Resources
{
    public class ResourceKey
    {
        public ResourceKey(string identifier, IPermission permission)
        {
            Identifier = identifier;

            Permission = permission;
        }

        public string Identifier { get; private set; }
        public IPermission Permission { get; private set; }

        public override string ToString()
        {
            return Identifier;
        }

        public static implicit operator string(ResourceKey resourceKey)
        {
            return resourceKey.Identifier;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as ResourceKey;

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
