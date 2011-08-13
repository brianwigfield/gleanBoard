using System.Linq;
using MongoDB.Driver;

namespace gleanBoard
{
    public class ViewRepository
    {
        MongoDatabase data;

        public ViewRepository()
        {
            data = MongoServer.Create("mongodb://localhost").GetDatabase("Views");
        }

        public void Save<T>(T view)
        {
            data.GetCollection("Views").Save(view);
        }

        public T Get<T>(/*IQueryable<T> filter*/)
        {
            return data.GetCollection("Views").FindOneAs<T>();
        }
    }
}