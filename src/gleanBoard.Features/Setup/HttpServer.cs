using System;
using TechTalk.SpecFlow;

namespace gleanBoard.Features.Setup
{
    [Binding]
    public class WebServer
    {
        static HttpListenerHostWithConfiguration _host;

        public static Uri Site
        {
            get { return new Uri(string.Format("http://localhost:{0}", 9222)); }
        }

        [BeforeTestRun]
        public static void Setup()
        {
            _host = new HttpListenerHostWithConfiguration { Configuration = new Configuration() };
            _host.Initialize(new[] { "http://+:9222/" }, "/", null);
            _host.StartListening();
        }

        [AfterTestRun]
        public static void ShutDown()
        {
            _host.StopListening();
            _host.Close();
        }

    }

}
