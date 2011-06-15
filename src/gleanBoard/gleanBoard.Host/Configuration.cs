using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using gleanBoard.Handlers;
using gleanBoard.Resources;
using OpenRasta.Codecs.Razor;
using OpenRasta.Configuration;

namespace gleanBoard.Host
{
    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.ViewsEmbeddedInTheAssembly(Assembly.GetExecutingAssembly(), "gleanBoard.Hosts.Views");

                ResourceSpace.Has.ResourcesOfType<Home>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByRazor(new { index = "Home.cshtml" })
                    .ForMediaType(OpenRasta.Web.MediaType.Html);
            }
        }
    }
}
