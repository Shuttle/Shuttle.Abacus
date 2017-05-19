using System.Drawing;
using Shuttle.Abacus.Shell.Core.Configuration;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public class ValidationConfiguration : IValidationConfiguration
    {
        private static Color ErrorColorFlyWeight = Color.MistyRose;

        public ValidationConfiguration(IWindowsApplicationConfiguration windowsApplicationConfiguration)
        {
            SetErrorColor(windowsApplicationConfiguration);
        }

        public Color ErrorColor => ErrorColorFlyWeight;

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
