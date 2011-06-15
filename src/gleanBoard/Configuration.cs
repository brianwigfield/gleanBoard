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
                var ll = new OpenRasta.Codecs.WebForms.ResourceView<Board>();
                ResourceSpace.Has.ResourcesOfType<Board>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByRazor(new {index = "~/Views/Board.cshtml"})
                    .ForMediaType(OpenRasta.Web.MediaType.Html);

                    //.RenderedByAspx("~/Views/Home.aspx");
                    //.ForMediaType(OpenRasta.Web.MediaType.Html);
            }
        }
    }

    public class Home2
    {
        public string Blob { get; set; }
    }
}