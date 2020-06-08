using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Repositories
{
    public class DataRepository : IDataRepository
    {
        static List<Data> staticData = new List<Data>()
        {
            new Data()
            {
                Id=10,
                FirstName="mm"
            },
            new Data()
            {
                Id=11,
                FirstName="ss"
            }
        };
        public int korak = 5;
        public void PromeniKorak(int k)
        {
            korak = k;
        }
        public int vratiKorak()
        {
            return korak;
        }

        public void AddData(Data k)
        {
            staticData.Add(k);
        }

        public List<Data> getData()
        {
            return staticData;
        }
    }
}
