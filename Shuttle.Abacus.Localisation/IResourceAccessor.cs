using System.Drawing;

namespace Shuttle.Abacus.Localisation
{
    public interface IResourceAccessor
    {
        string Text { get; }
        Image Image { get; }
    }
}
