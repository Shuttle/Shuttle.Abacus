using System.Drawing;

namespace Shuttle.Abacus.Domain
{
    public interface IResourceAccessor
    {
        string Text { get; }
        Image Image { get; }
    }
}
