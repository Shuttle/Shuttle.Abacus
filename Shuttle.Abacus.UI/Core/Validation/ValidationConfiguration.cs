using System;
using System.Configuration;
using System.Drawing;

namespace Abacus.UI
{
    public class ValidationConfiguration : IValidationConfiguration
    {
        private static Color ErrorColorFlyWeight = Color.MistyRose;

        public ValidationConfiguration(IWindowsApplicationConfiguration windowsApplicationConfiguration)
        {
            SetErrorColor(windowsApplicationConfiguration);
        }

        public Color ErrorColor { get { return ErrorColorFlyWeight; } }

        private static void SetErrorColor(IWindowsApplicationConfiguration configuration)
        {
            try
            {
                ErrorColorFlyWeight =
                    ColorTranslator.FromHtml(configuration.ValidationErrorColor);

                if (ErrorColorFlyWeight == Color.Empty)
                {
                    ErrorColorFlyWeight = Color.MistyRose;
                }
            }
            catch
            {
                // swallow
            }
        }
    }
}
