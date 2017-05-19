using System;
using System.Windows.Forms;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.UI.Shell
{
    public partial class NotificationView : 
        Form,
        IMessageHandler<ResultNotificationMessage>
    {
        public NotificationView()
        {
            InitializeComponent();

            CloseButton.Image = Resources.Image_Exit;

            KeyUp += NotificationView_KeyUp;
        }

        void NotificationView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 27:
                    {
                        Close();

                        break;
                    }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void HandleMessage(ResultNotificationMessage message)
        {
            NotificationArea.Text = message.Result.ToString();
        }
    }
}
