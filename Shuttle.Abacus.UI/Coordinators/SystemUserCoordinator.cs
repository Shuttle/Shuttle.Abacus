using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Coordinators.Interfaces;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Messages.SystemUser;
using Shuttle.Abacus.UI.Models;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace;
using Shuttle.Abacus.UI.UI.SystemUser;
using Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Coordinators
{
    public class SystemUserCoordinator : Coordinator, ISystemUserCoordinator
    {
        private ISystemUserQuery _systemUserQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public SystemUserCoordinator(IDatabaseContextFactory databaseContextFactory, ISystemUserQuery systemUserQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(systemUserQuery, "systemUserQuery");

            _databaseContextFactory = databaseContextFactory;
            _systemUserQuery = systemUserQuery;
        }
        
        public void HandleMessage(ListSystemUserMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item = WorkItemManager
                    .Create("Users")
                    .ControlledBy<ISystemUserListController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<ISimpleListPresenter>().WithModel(new SimpleListModel(_systemUserQuery.All()))
                    .AddNavigationItem(NavigationItemFactory.Create<EditLoginNameMessage>())
                    .AddNavigationItem(NavigationItemFactory.Create<EditPermissionsMessage>());

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(NewSystemUserMessage message)
        {
            var item = WorkItemManager
                .Create("New user")
                .ControlledBy<ISystemUserController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ISystemUserPresenter>()
                .AddPresenter<IPermissionsPresenter>()
                .AddNavigationItem(
                NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public void HandleMessage(EditLoginNameMessage message)
        {
            using (_databaseContextFactory.Create())
            {
                var item =
                    WorkItemManager
                        .Create("Edit user")
                        .ControlledBy<ISystemUserController>()
                        .ShowIn<IContextToolbarPresenter>()
                        .AddPresenter<ISystemUserPresenter>().WithModel(_systemUserQuery.Get(message.SystemUserId))
                        .AddNavigationItem(
                            NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).AsDefault()
                        .AssignInitiator(message);

                item.GetPresenter<ISystemUserPresenter>().HandleMessage(message);

                HostInWorkspace<ITabbedWorkspacePresenter>(item);
            }
        }

        public void HandleMessage(EditPermissionsMessage message)
        {
            var item =
                WorkItemManager
                    .Create(string.Format("Permissions: {0}", message.LoginName))
                    .ControlledBy<ISystemUserController>()
                    .ShowIn<IContextToolbarPresenter>()
                    .AddPresenter<IPermissionsPresenter>().WithModel(_systemUserQuery.GetPermissions(message.SystemUserId))
                    .AddNavigationItem(NavigationItemFactory.Create(message).AssignResourceItem(ResourceItems.Submit)).
                    AsDefault()
                    .AssignInitiator(message);

            HostInWorkspace<ITabbedWorkspacePresenter>(item);
        }

        public static class StateKeys
        {
            public static readonly StateKey SystemUserId = new StateKey("SystemUserId");
        }
    }
}
