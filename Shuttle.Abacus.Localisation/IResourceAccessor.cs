using System.Drawing;

namespace Abacus.Localisation
{
    public interface IResourceAccessor
    {
        string Text { get; }
        Image Image { get; }
    }
}
