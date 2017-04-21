using System.Drawing;

namespace Shuttle.Abacus
{
    public interface IResourceAccessor
    {
        string Text { get; }
        Image Image { get; }
    }
}
