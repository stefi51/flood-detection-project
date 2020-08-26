using AnalyticsMicroservice.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.RefinedDataRepository
{
    public class RefinedDataRepository : IRefinedDataRepository
    {
        private readonly IMongoCollection<RefinedData> refinedCollection;
        public RefinedDataRepository(IOptions<RefinedDataDatabaseConfiguration> configuration)
        {
            // var client = new MongoClient(configuration.Value.ConnectionString);
            // var database = client.GetDatabase(configuration.Value.DatabaseName);
            // refinedCollection = database.GetCollection<RefinedData>(configuration.Value.RefinedDataCollectionName);
        }

        public List<RefinedData> GetAll()
        {
           return refinedCollection.Find(refined => true).ToList();
        }

        public void InsertData(RefinedData k)
        {
            // refinedCollection.InsertOne(k);
        }
    }
}
