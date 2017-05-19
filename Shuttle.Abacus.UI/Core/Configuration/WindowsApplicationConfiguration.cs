using System;
using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Configuration
{
    public class WindowsApplicationConfiguration : IWindowsApplicationConfiguration
    {
        private static readonly ConfigurationItem<string> validationErrorColor =
            new ConfigurationItem<string>(Convert.ToString(ConfigurationManager.AppSettings["ValidationErrorColor"]));


        public string ValidationErrorColor => validationErrorColor.GetValue();
    }
}
