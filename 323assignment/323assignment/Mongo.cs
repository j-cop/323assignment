using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _323assignment
{
    class Mongo
    {
        MongoClient dbClient;
        public Boolean Connect()
        {
            try
            {
                dbClient = new MongoClient("mongodb://compx323-05:iZAvZVGP6w@mongodb.cms.waikato.ac.nz");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<BsonDocument> Read(string collection, BsonDocument filter)
        {
            IMongoDatabase database = dbClient.GetDatabase("compx323-05");
            IMongoCollection<BsonDocument> doc = database.GetCollection<BsonDocument>(collection);

            return doc.Find(filter).ToList();

        }
        public List<BsonDocument> Read(string collection)
        {
            return Read(collection, new BsonDocument());

        }

        public void Insert(string collection, BsonDocument insertDoc)
        {
            IMongoDatabase database = dbClient.GetDatabase("compx323-05");
            IMongoCollection<BsonDocument> doc = database.GetCollection<BsonDocument>(collection);
            doc.InsertOne(insertDoc);
        }
    }
}
