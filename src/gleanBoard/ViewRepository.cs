using System.Configuration;
using MongoDB.Driver;

namespace gleanBoard
{
    public class ViewRepository
    {
        MongoDatabase data;

        public ViewRepository()
        {
            var connection = new MongoUrlBuilder(ConfigurationManager.AppSettings["MONGOHQ_URL"]);
            data = MongoServer.Create(connection.ToMongoUrl()).GetDatabase(connection.DatabaseName);
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