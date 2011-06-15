using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class HomeHandler
    {
        public object Get()
        {
            return new Board
                       {
                           Lanes = new List<Lane>
                                       {
                                           new Lane
                                               {
                                                   Name = "BackLog",
                                                   Cards = new List<string>(new[] {"My first card", "My second card"})
                                               },
                                           new Lane {Name = "WorkingOn", Cards = new List<string>(new [] {"Blah card"})},
                                           new Lane {Name = "Completed", Cards = new List<string>()}
                                       }
                       };
        }
    }
}