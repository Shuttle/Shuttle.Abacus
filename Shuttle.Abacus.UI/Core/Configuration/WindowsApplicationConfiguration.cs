using System;
using System.Configuration;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class WindowsApplicationConfiguration : IWindowsApplicationConfiguration
    {
        private static readonly ConfigurationItem<string> validationErrorColor =
            new ConfigurationItem<string>(Convert.ToString(ConfigurationManager.AppSettings["ValidationErrorColor"]));


        public string ValidationErrorColor
        {
            get { return validationErrorColor.GetValue(); }
        }

    }
}
