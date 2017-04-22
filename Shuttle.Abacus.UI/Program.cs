using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI
{
    internal static class Program
    {
        public static IServiceBus Bus { get; private set; }

        private static bool loginRequestCompleted;
        private static IMessageBus messageBus;
        private static Splash splash;
        private static Thread splashThread;

        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoad;

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Application.ThreadException += ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();

            splashThread = new Thread(ShowSplash);

            splashThread.Start();

            DependencyWiring.Start().AddWindowsComponents().AddCaching();

            StartBus();

            // get an initial application instance to handle global events
            DependencyResolver.Resolve<IMessageBus>()
                .AddSubscribers
                (
                new List<object>
                    (
                    DependencyResolver.Resolver.ResolveAssignable<ICoordinator>().Cast<object>()
                    )
                );

            messageBus = DependencyResolver.Resolve<IMessageBus>();

            messageBus.Publish(new StartShellMessage());

            Login(string.Format(@"{0}\{1}",
                                Environment.UserDomainName,
                                Environment.UserName));

            if (!loginRequestCompleted)
            {
                CloseSplash();

                MessageBox.Show("It took too long to try to log you on.  Please try again.", "Login Failure",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Application.Exit();

                return;
            }

            messageBus.Publish(new ActivateShellMessage());

            Application.Run();
        }

        private static void CloseSplash()
        {
            splash.Invoke(new MethodInvoker(splash.Close));

            splashThread.Join();
        }

        private static void StartBus()
        {
            Bus = Configure.With()
                .CastleWindsorBuilder((IWindsorContainer) DependencyResolver.Resolver.Container)
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(true)
                .UnicastBus()
                .ImpersonateSender(false)
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }

        private static void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            ShowStatus("loaded", args.LoadedAssembly.GetName().Name);
        }

        private static void ShowStatus(string heading, string message)
        {
            splash.Invoke(new MethodInvoker(() => splash.ShowStatus(heading, message)));
        }

        private static void Login(string loginName)
        {
            ShowStatus("logging in", loginName);

            var command = new LoginCommand
                          {
                              LoginName = loginName
                          };

            Bus.Send(command).Register(ReplyCallback, command).AsyncWaitHandle.WaitOne(10000);
        }

        private static void ReplyCallback(IAsyncResult ar)
        {
            loginRequestCompleted = true;

            CloseSplash();

            var result = ar.AsyncState as CompletionResult;

            if (result == null)
            {
                return;
            }

            if (result.Messages == null)
            {
                return;
            }

            if (result.Messages.Length == 0)
            {
                return;
            }

            if (result.State == null)
            {
                return;
            }

            var reply = result.Messages[0] as ReplyMessage;

            if (reply == null)
            {
                return;
            }

            if (reply.Result.HasMessages)
            {
                messageBus.Publish(reply.Result);
            }
        }

        private static void ShowSplash()
        {
            splash = new Splash();

            splash.ShowDialog();
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowException(e.Exception.ToString());
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowException(e.ExceptionObject.ToString());
        }

        private static void ShowException(string message)
        {
            MessageBox.Show(message, "Application Exception", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
        }
    }
}
