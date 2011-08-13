using System;
using System.Collections.Generic;

namespace gleanBoard.Resources
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Lane> Lanes { get; set; }
    }

    public class Lane
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }

    public class Card
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

}