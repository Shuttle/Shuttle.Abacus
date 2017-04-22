using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Castle.Windsor;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Core.Castle;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI
{
    internal static class Program
    {
        private static WindsorContainer _container = new WindsorContainer();
        private static IServiceBus _bus;

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

            DependencyWiring.Start(_container).AddWindowsComponents().AddCaching();

            StartServiceBus();

            // get an initial application instance to handle global events
            DependencyResolver.Resolve<IMessageBus>()
                .AddSubscribers
                (
                new List<object>
                    (
                    DependencyResolver.Resolver.ResolveAssignable<ICoordinator>()
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

            _bus.Dispose();
            _container.Dispose();
        }

        private static void CloseSplash()
        {
            splash.Invoke(new MethodInvoker(splash.Close));

            splashThread.Join();
        }

        private static void StartServiceBus()
        {
            var container = new WindsorComponentContainer(_container);

            ServiceBus.Register(container);

            _bus = ServiceBus.Create(container).Start();
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

            _bus.Send(command);
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
