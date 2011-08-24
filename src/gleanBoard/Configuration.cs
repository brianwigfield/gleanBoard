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

                ResourceSpace.Has.ResourcesOfType<Home>()
                    .AtUri("/")
                    .HandledBy<HomeHandler>()
                    .RenderedByRazor(new {index = "~/Views/Index.cshtml"})
                    .ForMediaType(OpenRasta.Web.MediaType.Html);

                ResourceSpace.Has.ResourcesOfType<Board>()
                    .AtUri("/board")
                    .HandledBy<BoardHandler>()
                    .RenderedByRazor(new { index = "~/Views/Board.cshtml" })
                    .ForMediaType(OpenRasta.Web.MediaType.Html);

                ResourceSpace.Has.ResourcesOfType<RebuildViews>()
                    .AtUri("/tools/rebuildviews")
                    .HandledBy<RebuildViewsHandler>()
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<Signup>()
                    .AtUri("/signup")
                    .HandledBy<SignUpHandler>()
                    .RenderedByRazor(new {index = "~/Views/SignUp.cshtml"})
                    .ForMediaType(OpenRasta.Web.MediaType.Html)
                    .And
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<NewCard>()
                    .AtUri("/card/create")
                    .HandledBy<CardHandler>()
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<NewLane>()
                    .AtUri("/lane/create")
                    .HandledBy<LaneHandler>()
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<CardMoved>()
                    .AtUri("/card/move")
                    .HandledBy<MoveCardHandler>()
                    .AsJsonDataContract();



                //.RenderedByAspx("~/Views/Home.aspx");
                //.ForMediaType(OpenRasta.Web.MediaType.Html);
            }
        }
    }

    public class Signup
    {
        public bool Result { get; set; }
    }

    public class RebuildViews
    {
        public bool Result { get; set; }
    }

    public class CardMoved
    {
        public bool Result { get; set; }
    }
}