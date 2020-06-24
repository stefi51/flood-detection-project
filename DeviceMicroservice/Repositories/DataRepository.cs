using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Repositories
{
    public class DataRepository : IDataRepository
    {
        private static List<Data> staticData = new List<Data>();
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

        public void SetData(List<Data> list)
        {
            staticData = list;
        }
    }
}
