using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _323assignment
{
    class Mongo
    {
        public Boolean Connect()
        {
            try
            {
                MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
