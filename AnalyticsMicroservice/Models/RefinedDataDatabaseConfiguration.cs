using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Models
{
    public class RefinedDataDatabaseConfiguration
    {
        public string RefinedDataCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
