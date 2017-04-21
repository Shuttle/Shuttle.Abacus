using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Abacus.Application;
using Abacus.Data;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.UI;
using Abacus.Validation;
using Clipboard=Abacus.UI.Clipboard;

namespace Abacus.Windsor
{
    public class DependencyWiring : IDependencyWiringOptional
    {
        private readonly WindsorContainer container = new WindsorContainer();

        private DependencyWiring()
        {
        }

        public IDependencyWiringOptional AddWindowsComponents()
        {
            container.Register(Component.For<IShell>().ImplementedBy<Shell>());
            container.Register(Component.For<ISession>().ImplementedBy<Session>());
            container.Register(Component.For<IClipboard>().ImplementedBy<Clipboard>());

            container.Register
                (
                Component.For<IMessageResourceItemStore>().ImplementedBy<MessageResourceItemStore>(),
                Component.For<INavigationItemFactory>().ImplementedBy<NavigationItemFactory>(),
                Component.For<INavigationMap>().ImplementedBy<DefaultNavigationMap>(),
                Component.For<IExplorerRootItemOrderProvider>().ImplementedBy<ExplorerRootItemOrderProvider>()
                );

            container.Register
                (
                Component.For<IWindowsApplicationConfiguration>().ImplementedBy<WindowsApplicationConfiguration>(),
                Component.For<IValidationConfiguration>().ImplementedBy<ValidationConfiguration>(),
                Component.For<IControlValidatorProvider>().ImplementedBy<ControlValidatorProvider>(),
                Component.For<IViewValidatorFactory>().ImplementedBy<ViewValidatorFactory>(),
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(
                    type =>
                    !type.IsAbstract && !type.IsInterface && type.Name != "ViewValidator" &&
                    type.Name.EndsWith("Validator"))
                    .WithService.FirstInterface()
                );

            container.Register
                (
                Component.For<IBinderProvider>().ImplementedBy<BinderProvider>(),
                Component.For<IBinder<ListView>>().ImplementedBy<ListViewBinder>()
                );

            container.Register(Component.For<IMessageBus>().ImplementedBy<MessageBus>());
            container.Register
                (
                Component.For<IWorkItemManager>().ImplementedBy<WorkItemManager>(),
                Component.For<IWorkItemControllerFactory>().ImplementedBy<WorkItemControllerFactory>(),
                Component.For<IPresenterFactory>().ImplementedBy<PresenterFactory>(),
                Component.For<IWorkItemPresenterFactory>().ImplementedBy<WorkItemPresenterFactory>(),
                Component.For<IWorkItemBuilder>().ImplementedBy<WorkItemBuilder>().LifeStyle.Transient,
                Component.For<IWorkspaceProvider>().ImplementedBy<WorkspaceProvider>()
                );

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Service"))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(
                    type =>
                    !type.IsInterface && type.Name != "Presenter" && !type.Name.EndsWith("PartialPresenter") &&
                    type.Name.EndsWith("Presenter"))
                    .WithService.Select((type, basetype) => FindInterface("Presenter", type))
                    .Configure(configurer => configurer.LifeStyle.Transient));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(
                    type =>
                    !type.IsInterface && type.Name != "View" && !type.Name.EndsWith("PartialView") &&
                    type.Name.EndsWith("View") && !type.Name.StartsWith("Generic"))
                    .WithService.Select((type, basetype) => FindInterface("View", type))
                    .Configure(configurer => configurer.LifeStyle.Transient));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(type => !type.IsInterface && type.Name != "Coordinator" && type.Name.EndsWith("Coordinator"))
                    .WithService.Select((type, basetype) => FindInterface("Coordinator", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(
                    type => !type.IsInterface && type.Name != "WorkItemController" && type.Name.EndsWith("Controller"))
                    .WithService.Select((type, basetype) => FindInterface("Controller", "WorkItemController", type))
                    .Configure(configurer => configurer.LifeStyle.Transient));

            container.Register(Component.For<ISummaryViewManager>().ImplementedBy<SummaryViewManager>());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.UI")
                    .If(type => !type.IsInterface && type.Name != "Coordinator" && type.Name.EndsWith("Coordinator"))
                    .WithService.Select((type, basetype) => FindInterface("Coordinator", type)));

            return this;
        }

        public IDependencyWiringOptional AddWebComponents()
        {
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Endpoints")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Handler"))
                    .WithService.FirstInterface());


            ((IWindsorContainer) DependencyResolver.Resolver.Container).Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Endpoints")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Controller"))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name.ToLower()))
                    .Configure(configurer => configurer.LifeStyle.Transient)
                    .WithService.FirstInterface());

            return this;
        }

        public IDependencyWiringOptional AddServerComponents()
        {
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.SqlServer.Repository")
                    .If(type => type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            container.Register(Component.For<IORMMapperRegistry>().ImplementedBy<ORMMapperRegistry>());
            container.Register(Component.For<IORM>().ImplementedBy<ORM>());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.SqlServer.Repository")
                    .If(type => type.Name.EndsWith("Repository"))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name.ToLower()))
                    .WithService.Select((type, basetype) => FindInterface("Repository", type)));

            container.Register(Component.For<IRepositoryProvider>().ImplementedBy<RepositoryProvider>());

            // Domain
            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Domain")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Domain")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Provider"))
                    .WithService.Select((type, basetype) => FindInterface("Provider", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Domain")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Factory"))
                    .WithService.Select((type, basetype) => FindInterface("Factory", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Domain")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Service"))
                    .WithService.Select((type, basetype) => FindInterface("Service", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.DomainEventHandlers")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Handler"))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Policy")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Policy"))
                    .WithService.Select((type, basetype) => FindInterface("Policy", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Application")
                    .If(type => type.Name.EndsWith("Service"))
                    .WithService.Select((type, basetype) => FindInterface("Service", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Application")
                    .If(type => type.Name.EndsWith("ArgumentAnswerProvider"))
                    .WithService.Select((type, basetype) => FindGenericInterface(typeof(IPipe<>), type)));

            container.Register(Component.For<ITaskFactory>().ImplementedBy<TaskFactory>());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Application")
                    .If(type => type.Name.EndsWith("Task"))
                    .WithService.Select((type, basetype) => FindInterface("Task", type))
                    .Configure(configurer => configurer.LifeStyle.Transient));

            DomainEvents.Container = DependencyResolver.Resolver;

            return this;
        }

        public IDependencyWiringOptional AddCaching()
        {
            container.Register
                (
                Component.For<ICache>().ImplementedBy<DefaultCache>()
                );

            return this;
        }

        public IDependencyWiringOptional AddNullCaching()
        {
            container.Register
                (
                Component.For<ICache>().ImplementedBy<NullCache>()
                );

            return this;
        }

        public IDependencyWiringOptional InitializeORM()
        {
            container.Resolve<IORMMapperRegistry>().InitializeWith(DependencyResolver.Resolver);

            return this;
        }

        public static IDependencyWiringOptional Start()
        {
            return new DependencyWiring().AddCore();
        }


        private IDependencyWiringOptional AddCore()
        {
            // Infrastructure
            container.Register
                (
                Component.For<IObjectSerializer>().ImplementedBy<XmlObjectSerializer>()
                );

            container.Register(Component.For<ILogFactory>().ImplementedBy<Log4NetLogFactory>());
            container.Register(Component.For<IImageService>().ImplementedBy<ImageService>());
            container.Register(Component.For<IApplicationConfiguration>().ImplementedBy<ApplicationConfiguration>());

            // Data Access
            container.Register(Component.For<IDatabaseGateway>().ImplementedBy<DatabaseGateway>());
            container.Register(Component.For<IDbConnectionConfiguration>().ImplementedBy<DbConnectionConfiguration>());
            container.Register(Component.For<IQueryProcessor>().ImplementedBy<QueryProcessor>());
            container.Register(Component.For<IDatabaseConnectionProvider>().ImplementedBy<DatabaseConnectionProvider>());
            container.Register(Component.For<IUnitOfWorkProvider>().ImplementedBy<UnitOfWorkProvider>());

            container.Register
                (
                Component.For(typeof (IDataReaderRepository<>)).ImplementedBy(typeof (DataReaderRepository<>)),
                Component.For(typeof (IDataRowRepository<>)).ImplementedBy(typeof (DataRowRepository<>)),
                Component.For(typeof (IDataTableRepository<>)).ImplementedBy(typeof (DataTableRepository<>))
                );

            container.Register
                (
                Component.For<IDbDataParameterFactory>().ImplementedBy<SqlServerDbDataParameterFactory>(),
                Component.For<IInsertBuilderFactory>().ImplementedBy<SqlServerInsertBuilderFactory>(),
                Component.For<IUpdateBuilderFactory>().ImplementedBy<SqlServerUpdateBuilderFactory>(),
                Component.For<IDeleteBuilderFactory>().ImplementedBy<SqlServerDeleteBuilderFactory>(),
                Component.For<ISelectBuilderFactory>().ImplementedBy<SqlServerSelectBuilderFactory>(),
                Component.For<IWhereBuilderFactory>().ImplementedBy<SqlServerWhereBuilderFactory>(),
                Component.For<IContainsBuilderFactory>().ImplementedBy<SqlServerContainsBuilderFactory>()
                );

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Data.Core")
                    .If(type => type.Name.EndsWith("Factory"))
                    .WithService.FirstInterface()
                );

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.SqlServer.Query")
                    .If(type => type.Name.EndsWith("Query"))
                    .WithService.Select((type, basetype) => FindInterface("Query", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.SqlServer.Query")
                    .If(type => type.Name.EndsWith("Mapper"))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Data.Query")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Pipe"))
                    .WithService.Select((type, basetype) => FindGenericInterface(typeof(IPipe<>), type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Validation")
                    .If(type => !type.IsInterface && type.Name.EndsWith("Rules"))
                    .WithService.Select((type, basetype) => FindInterface("Rules", type)));

            container.Register(
                AllTypes.Pick()
                    .FromAssemblyNamed("Abacus.Validation")
                    .If(type => !type.IsInterface && type.Name.EndsWith("ValueTypeValidator"))
                    .WithService.Select((type, basetype) => FindInterface("ValueTypeValidator", type)));

            container.Register
                (
                Component.For(typeof (IFactoryProvider<>)).ImplementedBy(typeof (FactoryProvider<>)),
                Component.For<IValueTypeValidatorProvider>().ImplementedBy<ValueTypeValidatorProvider>(),
                Component.For<IPipeline>().ImplementedBy<Pipeline>()
                );


            QueryColumn.SetDbDataParameterFactory(container.Resolve<IDbDataParameterFactory>());

            DependencyResolver.InitializeWith(new WindsorResolver(container));

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
        IDependencyWiringOptional AddServerComponents();
        IDependencyWiringOptional AddCaching();
        IDependencyWiringOptional AddNullCaching();
        IDependencyWiringOptional InitializeORM();
    }
}
