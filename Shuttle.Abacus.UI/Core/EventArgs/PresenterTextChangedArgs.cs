using System;

namespace Abacus.UI
{
    public class PresenterTextChangedArgs : EventArgs
    {
        public string From { get; private set; }
        public string To { get; private set; }

        public PresenterTextChangedArgs(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}
