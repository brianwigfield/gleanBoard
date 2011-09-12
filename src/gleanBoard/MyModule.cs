using System;
using System.Web.Compilation;
using gleanBoard.Handlers;
using Nancy;
using Nancy.ModelBinding;

namespace gleanBoard
{

    public class MyModule : NancyModule
    {

        public MyModule()
        {

            Get["/"] = _ =>
                View["Views/Home.cshtml"];

            Get["/board"] = _ =>
                {
                    return View["Views/Board.cshtml", new BoardHandler().Get()];
                };

            Get["/tools/rebuildviews/{AsOf}"] = _ =>
                {
                    var asOf = DateTime.Now;
                    DateTime.TryParse(_.AsOf, out asOf);
                    return View[new RebuildViewsHandler().Get(new RebuildData{ AsOf = asOf })];
                };

            Get["/signup"] = _ =>
                {
                    return View["Views/SignUp.cshtml"];
                };

            Post["/signup"] = _ =>
                {
                    return Response.AsJson(new SignUpHandler().Post(this.Bind<SignUpData>()));
                };

            Post["/card/create"] = _ =>
                {
                    return Response.AsJson(new CardHandler().Post(this.Bind<NewCardData>()));
                };

            Post["/card/move"] = _ =>
                {
                    return Response.AsJson(new MoveCardHandler().Post(this.Bind<MoveCardHandler.MoveLaneData>()));
                };

            Post["/lane/create"] = _ =>
                {
                    return Response.AsJson(new LaneHandler().Post(this.Bind<NewLaneData>()));
                };
        }

    }
}