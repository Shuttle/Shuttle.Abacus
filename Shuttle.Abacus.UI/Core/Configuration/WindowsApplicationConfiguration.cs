using System;
using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Configuration
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
