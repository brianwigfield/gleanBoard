using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gleanBoard.Resources
{
    public class Board
    {
        public string Id { get; set; }
        public List<Lane> Lanes { get; set; }
    }

    public class Lane
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public List<MongoDBRef> CardRefs { get; set; }
    }

    public class Card
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

}