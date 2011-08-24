using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class HomeHandler
    {
        public Home Get()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var server = MongoDB.Driver.MongoServer.Create("mongodb://wspec:13bb3158f6a382432c276f3cece1c11e@flame.mongohq.com:27061/BabyNames");
            var doc = server.GetDatabase("BabyNames").GetCollection("BabyName").FindOne();

            var stuff = "got id " + doc["_id"].AsObjectId;
            sw.Stop();
            stuff = stuff + " in " + sw.ElapsedMilliseconds + " ms";
            return new Home
                       {
                           Stuff = stuff
                       };
        }
    }
}