using System;
using System.Collections.Generic;
using System.Web;
using gleanBoard.Resources;
using SimpleCqrs;
using SimpleCqrs.Commanding;
using SimpleCqrs.Unity;

namespace gleanBoard
{

    public class CqrsRuntime : SimpleCqrs.SimpleCqrsRuntime<UnityServiceLocator>
    {
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

            var board = new Board {Lanes = new List<Lane>()};
            //{
                              //    new Lane{Id = Guid.NewGuid().ToString(), Name = "Backlog", Cards = new List<Card>()},
                              //    new Lane {Id = Guid.NewGuid().ToString(), Name = "Working On", Cards =new List<Card>()},
                              //    new Lane {Id = Guid.NewGuid().ToString(), Name = "Completed", Cards = new List<Card>()}
                              //};
            runtime.ServiceLocator.Register(board);

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
