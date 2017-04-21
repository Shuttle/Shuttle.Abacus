using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public class MoneyFormatter : IDisposable
    {
        private readonly Control valueProvider;
        private readonly Control display;

        public MoneyFormatter(Control valueProvider, Control display)
        {
            this.valueProvider = valueProvider;
            this.display = display;

            valueProvider.TextChanged += TextChanged;
        }

        private void TextChanged(object sender, EventArgs e)
        {
            decimal value;

            display.Text = decimal.TryParse(valueProvider.Text, out value)
                               ? string.Format("{0:c}", value)
                               : string.Empty;
        }

        public void Dispose()
        {
            valueProvider.TextChanged -= TextChanged;
        }
    }
}
