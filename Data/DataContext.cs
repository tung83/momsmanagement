using Microsoft.Extensions.Options;
using momsManagement.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace momsManagement.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Children> ChildrenCollection
        {
            get
            {
                return _database.GetCollection<Children>("Children");
            }
        }
    }
}