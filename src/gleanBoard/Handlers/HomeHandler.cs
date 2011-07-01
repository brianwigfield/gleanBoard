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
                                                   Id = Guid.NewGuid().ToString(),
                                                   Name = "Backlog",
                                                   Cards = new List<Card> { new Card {Id = Guid.NewGuid().ToString(), Title = "My first card"}, new Card {Id= Guid.NewGuid().ToString(), Title = "My second card"}}
                                               },
                                           new Lane {Name = "Working On", Cards = new List<Card> {new Card {Id= Guid.NewGuid().ToString(), Title = "Blah card"}}},
                                           new Lane {Name = "Completed", Cards = new List<Card>()}
                                       }
                       };
        }
    }
}