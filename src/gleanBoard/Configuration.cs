using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gleanBoard.Handlers;
using gleanBoard.Resources;
using OpenRasta.Configuration;
using OpenRasta.Codecs.Razor;

namespace gleanBoard
{
    public class Configuration : IConfigurationSource 
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                //OpenRasta.Codecs.Razor.RazorCodec
                ResourceSpace.Has.ResourcesOfType<Home>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByRazor(new {index = "~/Views/Home.cshtml"})
                    .ForMediaType(OpenRasta.Web.MediaType.Html);
            }
        }
    }
}