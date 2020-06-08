using AnalyticsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.RefinedDataRepository
{
    public interface IRefinedDataRepository
    {
        public List<RefinedData> GetAll();
        public void InsertData(RefinedData k);
        
    }
}
