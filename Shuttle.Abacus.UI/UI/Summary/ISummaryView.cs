using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public interface ISummaryView : IView
    {
        void Clear();
        void EnsureColumnCount(int count);
        void AddGroup(string name);
        void AddRow();
        void AddHeading(string text);
        void AddRow(object[] values);
        void AddAttribute(string text, object value);
    }
}
