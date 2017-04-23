using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abacus.Domain;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net;
using RabbitMQ.Client.Impl;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Castle;
using Shuttle.Core.Data;
using Shuttle.Core.Host;
using Shuttle.Core.Infrastructure;
using Shuttle.Core.Log4Net;
using Shuttle.Esb;
using TaskFactory = System.Threading.Tasks.TaskFactory;

namespace Shuttle.Abacus.Server
{
    public class Host : IHost, IDisposable
    {
        private IServiceBus _bus;
        private WindsorContainer _container;

        public void Dispose()
        {
            _bus.Dispose();
            _container.Dispose();
            LogManager.Shutdown();
        }

        public void Start()
        {
            Log.Assign(new Log4NetLog(LogManager.GetLogger(typeof(Host))));

            _container = new WindsorContainer();

            Wire();

            var container = new WindsorComponentContainer(_container);

            ServiceBus.Register(container);

            container.Resolve<IDatabaseContextFactory>().ConfigureWith("Sentinel");

            _bus = ServiceBus.Create(container).Start();
        }

        private void Wire()
        {
            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => type.Name.EndsWith("Repository"))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name.ToLower()))
                    .WithService.Select((type, basetype) => FindInterface("Repository", type)));

            _container.Register(Component.For<IRepositoryProvider>().ImplementedBy<RepositoryProvider>());

            // Domain
            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Provider") && !type.Name.EndsWith("ArgumentAnswerProvider"))
                    .WithService.Select((type, basetype) => FindInterface("Provider", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Factory"))
                    .WithService.Select((type, basetype) => FindInterface("Factory", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Service"))
                    .WithService.Select((type, basetype) => FindInterface("Service", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Handler"))
                    .WithService.FirstInterface());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Policy"))
                    .WithService.Select((type, basetype) => FindInterface("Policy", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => type.Name.EndsWith("ArgumentAnswerProvider"))
                    .WithService.Select((type, basetype) => FindGenericInterface(typeof(IPipe<>), type)));

            _container.Register(Component.For<ITaskFactory>().ImplementedBy<ApplicationService.TaskFactory>());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => type.Name.EndsWith("Task"))
                    .WithService.Select((type, basetype) => FindInterface("Task", type))
                    .LifestyleTransient());

            DomainEvents.Container = DependencyResolver.Resolver;
        }
    }
}