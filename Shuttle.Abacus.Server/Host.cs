using System;
using System.Collections.Generic;
using Abacus.Domain;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Server.EventHandlers;
using Shuttle.Core.Castle;
using Shuttle.Core.Data;
using Shuttle.Core.Host;
using Shuttle.Core.Infrastructure;
using Shuttle.Core.Log4Net;
using Shuttle.Esb;
using Shuttle.Recall;
using IPipeline = Shuttle.Core.Infrastructure.IPipeline;
using Pipeline = Shuttle.Core.Infrastructure.Pipeline;

namespace Shuttle.Abacus.Server
{
    public class Host : IHost, IDisposable
    {
        private IEventProcessor _eventProcessor;
        private IEventStore _eventStore;

        private IServiceBus _bus;
        private WindsorContainer _container;

        public void Dispose()
        {
            _bus.Dispose();
            _eventProcessor?.Dispose();
            _eventStore?.AttemptDispose();
            _container.Dispose();

            LogManager.Shutdown();
        }

        public void Start()
        {
            Log.Assign(new Log4NetLog(LogManager.GetLogger(typeof(Host))));

            _container = new WindsorContainer();

            Wire();

            var container = new WindsorComponentContainer(_container);

            RegisterEventStore(container);

            ServiceBus.Register(container);

            container.Resolve<IDatabaseContextFactory>().ConfigureWith("Abacus");

            _bus = ServiceBus.Create(container).Start();
        }

        private void RegisterEventStore(WindsorComponentContainer container)
        {
            EventStore.Register(container);

            _eventStore = EventStore.Create(container);

            container.Register<ArgumentHandler>();
            container.Register<FormulaHandler>();
            container.Register<SystemUserHandler>();
            container.Register<TestHandler>();

            _eventProcessor = container.Resolve<IEventProcessor>();

            _eventProcessor.AddProjection(new Projection("Argument").AddEventHandler(container.Resolve<ArgumentHandler>()));
            _eventProcessor.AddProjection(new Projection("Formula").AddEventHandler(container.Resolve<FormulaHandler>()));
            _eventProcessor.AddProjection(new Projection("Test").AddEventHandler(container.Resolve<TestHandler>()));
            //_eventProcessor.AddProjection(new Projection("SystemUser").AddEventHandler(container.Resolve<SystemUserHandler>()));

            _eventProcessor.Start();
        }

        private void Wire()
        {
            _container.Register
            (
                Component.For(typeof(IDataRepository<>)).ImplementedBy(typeof(DataRepository<>)),
                Component.For<IImageService>().ImplementedBy<ImageService>(),
                Component.For<IConstraintComparison>().ImplementedBy<ConstraintComparison>()
            );

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => type.Name.EndsWith("Query"))
                    .WithService.Select((type, basetype) => FindInterface("Query", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Rules"))
                    .WithService.Select((type, basetype) => FindInterface("Rules", type)));

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("ValueTypeValidator"))
                    .WithService.Select((type, basetype) => FindInterface("ValueTypeValidator", type)));

            _container.Register
            (
                Component.For(typeof(IFactoryProvider<>)).ImplementedBy(typeof(FactoryProvider<>)),
                Component.For<IValueTypeValidatorProvider>().ImplementedBy<ValueTypeValidatorProvider>(),
                Component.For<IPipeline>().ImplementedBy<Pipeline>()
            );

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

            // Domain
            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(
                        type =>
                            !type.IsInterface && type.Name.EndsWith("Provider") &&
                            !type.Name.EndsWith("ArgumentAnswerProvider"))
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
                    .If(type => type.Name.EndsWith("Task"))
                    .WithService.Select((type, basetype) => FindInterface("Task", type))
                    .LifestyleTransient());

            DependencyResolver.InitializeWith(new WindsorResolver(_container));
        }

        private static IEnumerable<Type> FindInterface(string suffix, Type type)
        {
            return FindInterface(suffix, suffix, type);
        }

        private static IEnumerable<Type> FindInterface(string suffix, string excludeSuffix, Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if (!i.IsGenericType && i.Name != string.Format("I{0}", excludeSuffix) &&
                    i.Name.EndsWith(suffix))
                {
                    return new List<Type>
                    {
                        i
                    };
                }
            }

            return null;
        }
    }
}