using System;
using System.Configuration;
using System.Linq;
using System.Web;
using SimpleCqrs.Eventing;
using SimpleCqrs;
using SimpleCqrs.Commanding;
using SimpleCqrs.Unity;

namespace gleanBoard
{

    public class CqrsRuntime : SimpleCqrsRuntime<UnityServiceLocator>
    {
        protected override IEventStore GetEventStore(IServiceLocator serviceLocator)
        {
            return new SimpleCqrs.EventStore.MongoDb.MongoEventStore(ConfigurationManager.AppSettings["MONGOHQ_URL"], serviceLocator.Resolve<ITypeCatalog>());
        }

        protected override System.Collections.Generic.IEnumerable<System.Reflection.Assembly> GetAssembliesToScan(IServiceLocator serviceLocator)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(_ => _.GlobalAssemblyCache == false);
        }
    }

    public static class Runtime
    {
        public static ICommandBus Bus { get; set; }
        public static IServiceLocator Locator { get; set; }
    }

    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            var runtime = new CqrsRuntime();
            runtime.Start();

            Runtime.Bus = runtime.ServiceLocator.Resolve<ICommandBus>();
            Runtime.Locator = runtime.ServiceLocator;

            var repo = new ViewRepository();
            runtime.ServiceLocator.Register(repo);

            //var ll = runtime.ServiceLocator.Resolve<IEventStore>().GetEventsByEventTypes(new List<Type> { typeof(LaneCreatedEvent) }, DateTime.Now.AddYears(-1), DateTime.Now);
            //runtime.ServiceLocator.Resolve<IEventBus>().PublishEvents(ll);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

    }
}
