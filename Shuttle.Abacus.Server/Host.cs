using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using log4net;
using Shuttle.Core.Castle;
using Shuttle.Core.Data;
using Shuttle.Core.Host;
using Shuttle.Core.Infrastructure;
using Shuttle.Core.Log4Net;
using Shuttle.Esb;

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

            //_container.RegisterDataAccessCore();
            //_container.RegisterDataAccess("Shuttle.Sentinel");

            var container = new WindsorComponentContainer(_container);

            ServiceBus.Register(container);

            container.Resolve<IDatabaseContextFactory>().ConfigureWith("Sentinel");

            _bus = ServiceBus.Create(container).Start();
        }
    }
}