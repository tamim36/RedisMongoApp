using System;
using System.Collections.Generic;
using System.Text;
using Model;
using MongoDB.Driver;

namespace Repository
{
    public class RepositoryManagement : IRepositoryManagement
    {
        //private readonly IMongoCollection<Student> _Collection;
        private IMongoDatabase db;

        public RepositoryManagement(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            db = client.GetDatabase(settings.DatabaseName);
            //_Collection = database.GetCollection<Student>(settings.StudentsCollectionName);
        }

        public T Get<T>(string Id)
        {
            string name = typeof(T).Name;
            var collectionName = $"{name}s";
            var collection = db.GetCollection<T>(name + "s");
            var filter = Builders<T>.Filter.Eq("PrimaryId", Id);
            return collection.Find(filter).First();
        }


        public T Create<T>(T record)
        {
            var collection = db.GetCollection<T>(record.GetType().Name + "s");

            collection.InsertOne(record);
            return record;
        }

        public void UpdateInfo<T>(string Id, T record)
        {
            string name = typeof(T).Name;
            var collection = db.GetCollection<T>(name + "s");
            var filter = Builders<T>.Filter.Eq("PrimaryId", Id);
            collection.ReplaceOne(filter, record, new ReplaceOptions() { IsUpsert = false });
        }
    }
}
