using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Shell.Core.Binding;
using Shuttle.Abacus.Shell.Core.Clipboard;
using Shuttle.Abacus.Shell.Core.Configuration;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Validation;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.Explorer;
using Shuttle.Core.Data;
using Clipboard=Shuttle.Abacus.Shell.Core.Clipboard.Clipboard;

namespace Shuttle.Abacus.Shell
{
    public class DependencyWiring : IDependencyWiringOptional
    {
        private readonly IWindsorContainer _container;

        private DependencyWiring(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyWiringOptional AddWindowsComponents()
        {
            _container.Register(Component.For<IApplicationShell>().ImplementedBy<ApplicationShell>());
            _container.Register(Component.For<ISession>().ImplementedBy<Session>());
            _container.Register(Component.For<IClipboard>().ImplementedBy<Clipboard>());

            _container.Register
                (
                Component.For<IMessageResourceItemStore>().ImplementedBy<MessageResourceItemStore>(),
                Component.For<INavigationItemFactory>().ImplementedBy<NavigationItemFactory>(),
                Component.For<INavigationMap>().ImplementedBy<DefaultNavigationMap>(),
                Component.For<IExplorerRootItemOrderProvider>().ImplementedBy<ExplorerRootItemOrderProvider>()
                );

            _container.Register
                (
                Component.For<IWindowsApplicationConfiguration>().ImplementedBy<WindowsApplicationConfiguration>(),
                Component.For<IValidationConfiguration>().ImplementedBy<ValidationConfiguration>(),
                Component.For<IControlValidatorProvider>().ImplementedBy<ControlValidatorProvider>(),
                Component.For<IViewValidatorFactory>().ImplementedBy<ViewValidatorFactory>(),
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(
                    type =>
                    !type.IsAbstract && !type.IsInterface && type.Name != "ViewValidator" &&
                    type.Name.EndsWith("Validator"))
                    .WithService.FirstInterface()
                );

            _container.Register
                (
                Component.For<IBinderProvider>().ImplementedBy<BinderProvider>(),
                Component.For<IBinder<ListView>>().ImplementedBy<ListViewBinder>()
                );

            _container.Register(Component.For<IMessageBus>().ImplementedBy<MessageBus>());
            _container.Register
                (
                Component.For<IWorkItemManager>().ImplementedBy<WorkItemManager>(),
                Component.For<IWorkItemControllerFactory>().ImplementedBy<WorkItemControllerFactory>(),
                Component.For<IPresenterFactory>().ImplementedBy<PresenterFactory>(),
                Component.For<IWorkItemPresenterFactory>().ImplementedBy<WorkItemPresenterFactory>(),
                Component.For<IWorkItemBuilder>().ImplementedBy<WorkItemBuilder>().LifeStyle.Transient,
                Component.For<IWorkspaceProvider>().ImplementedBy<WorkspaceProvider>()
                );

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Service"))
                    .WithService.FirstInterface());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(
                    type =>
                    !type.IsInterface && type.Name != "Presenter" && !type.Name.EndsWith("PartialPresenter") &&
                    type.Name.EndsWith("Presenter"))
                    .WithService.Select((type, basetype) => FindInterface("Presenter", type))
                    .LifestyleTransient());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(
                    type =>
                    !type.IsInterface && type.Name != "View" && !type.Name.EndsWith("PartialView") &&
                    type.Name.EndsWith("View") && !type.Name.StartsWith("Generic"))
                    .WithService.Select((type, basetype) => FindInterface("View", type))
                    .LifestyleTransient());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name != "Coordinator" && type.Name.EndsWith("Coordinator"))
                    .WithServiceAllInterfaces());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(
                    type => !type.IsInterface && type.Name != "WorkItemController" && type.Name.EndsWith("Controller"))
                    .WithService.Select((type, basetype) => FindInterface("Controller", "WorkItemController", type))
                    .LifestyleTransient());

            _container.Register(Component.For<ISummaryViewManager>().ImplementedBy<SummaryViewManager>());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.Shell")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name != "Coordinator" && type.Name.EndsWith("Coordinator"))
                    .WithService.Select((type, basetype) => FindInterface("Coordinator", type)));

            return this;
        }

        public IDependencyWiringOptional AddWebComponents()
        {
            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus.WebApi")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Controller"))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name.ToLower()))
                    .LifestyleTransient()
                    .WithService.FirstInterface());

            return this;
        }

        public IDependencyWiringOptional AddCaching()
        {
            _container.Register
                (
                Component.For<ICache>().ImplementedBy<DefaultCache>()
                );

            return this;
        }

        public IDependencyWiringOptional AddNullCaching()
        {
            _container.Register
                (
                Component.For<ICache>().ImplementedBy<NullCache>()
                );

            return this;
        }

        public static IDependencyWiringOptional Start(IWindsorContainer container)
        {
            return new DependencyWiring(container).AddCore();
        }


        private IDependencyWiringOptional AddCore()
        {
            _container.Register(Component.For<IImageService>().ImplementedBy<ImageService>());

            _container.Register
                (
                Component.For(typeof (IDataRepository<>)).ImplementedBy(typeof (DataRepository<>)),
                Component.For(typeof (IDataTableRepository<>)).ImplementedBy(typeof (DataTableRepository<>))
                );

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => type.Name.EndsWith("Factory"))
                    .WithService.FirstInterface()
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
                    .If(type => type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            _container.Register(
                Classes
                    .FromAssemblyNamed("Shuttle.Abacus")
                    .Pick()
                    .If(type => !type.IsInterface && type.Name.EndsWith("Pipe"))
                    .WithService.Select((type, basetype) => FindGenericInterface(typeof(IPipe<>), type)));

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
                Component.For(typeof (IFactoryProvider<>)).ImplementedBy(typeof (FactoryProvider<>)),
                Component.For<IValueTypeValidatorProvider>().ImplementedBy<ValueTypeValidatorProvider>(),
                Component.For<IPipeline>().ImplementedBy<Pipeline>()
                );



            DependencyResolver.InitializeWith(new WindsorResolver(_container));

            return this;
        }

        private static IEnumerable<Type> FindGenericInterface(Type generic, Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if (i.Name.Equals(generic.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return new List<Type>
                           {
                               i
                           };
                }
            }

            return null;
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

    public interface IDependencyWiringOptional
    {
        IDependencyWiringOptional AddWindowsComponents();
        IDependencyWiringOptional AddWebComponents();
        IDependencyWiringOptional AddCaching();
        IDependencyWiringOptional AddNullCaching();
    }
}
