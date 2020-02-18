using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HangFireConsole
{
    public class RepositoryConsole : IRepositoryConsole
    {
        private IMongoDatabase db;

        public RepositoryConsole(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        public List<T> GetRecords<T>()
        {
            string name = typeof(T).Name;
            var collection = db.GetCollection<T>(name + "s");
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
