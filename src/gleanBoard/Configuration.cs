﻿using System.Collections.Generic;
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

                ResourceSpace.Has.ResourcesOfType<Board>()
                    .AtUri("/home")
                    .And.AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByRazor(new {index = "~/Views/Board.cshtml"})
                    .ForMediaType(OpenRasta.Web.MediaType.Html);

                ResourceSpace.Has.ResourcesOfType<NewCard>()
                    .AtUri("/card/create")
                    .HandledBy<CardHandler>()
                    .AsJsonDataContract();

                //.RenderedByAspx("~/Views/Home.aspx");
                //.ForMediaType(OpenRasta.Web.MediaType.Html);
            }
        }
    }
}