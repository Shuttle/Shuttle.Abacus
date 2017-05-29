using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Castle.Windsor;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Core.Castle;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Shell
{
    internal static class Program
    {
        private static Splash _splash;

        private static readonly WindsorContainer _container = new WindsorContainer();
        private static IServiceBus _bus;

        private static IMessageBus _messageBus;
        private static Thread _splashThread;

        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoad;

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Application.ThreadException += ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();

            _splashThread = new Thread(ShowSplash);

            _splashThread.Start();

            var container = new WindsorComponentContainer(_container);

            EventStore.Register(container);
            ServiceBus.Register(container);

            _bus = ServiceBus.Create(container).Start();

            container.Resolve<IDatabaseContextFactory>().ConfigureWith("Abacus");

            DependencyWiring.Start(_container).AddWindowsComponents();

            _messageBus = DependencyResolver.Resolve<IMessageBus>();

            using (container.Resolve<IDatabaseContextFactory>().Create())
            {
                _messageBus.AddSubscribers
                (
                    new List<object>
                    (
                        DependencyResolver.Resolver.ResolveAssignable<ICoordinator>()
                    )
                );
            }


            _messageBus.Publish(new StartShellMessage());

            Login(string.Format(@"{0}\{1}",
                Environment.UserDomainName,
                Environment.UserName));

            Application.Run();

            Dispose();
            Environment.Exit(0);
        }

        private static void Dispose()
        {
            _bus?.Dispose();
            _container?.Dispose();
        }

        public static void CloseSplash()
        {
            if (_splash.Created)
            {
                _splash.Invoke(new MethodInvoker(_splash.Close));
            }

            _splashThread.Join();
        }

        private static void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            ShowStatus("loaded", args.LoadedAssembly.GetName().Name);
        }

        private static void ShowStatus(string heading, string message)
        {
            if (!_splash.Created)
            {
                return;
            }

            _splash.Invoke(new MethodInvoker(() => _splash.ShowStatus(heading, message)));
        }

        private static void Login(string loginName)
        {
            ShowStatus("logging in", loginName);

            var command = new LoginCommand
            {
                LoginName = loginName
            };

            _bus.Send(new LoginTimeoutCommand(), c => c.Defer(DateTime.Now.AddSeconds(5)).Local());
            _bus.Send(command);
        }

        private static void ShowSplash()
        {
            _splash = new Splash();

            _splash.ShowDialog();
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