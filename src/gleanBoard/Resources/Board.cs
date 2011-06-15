using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gleanBoard.Resources
{
    public class Board
    {
        public ObjectId Id { get; set; }
        public List<Lane> Lanes { get; set; }
    }

    public class Lane
    {

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public List<MongoDBRef> CardRefs { get; set; }

    }
}