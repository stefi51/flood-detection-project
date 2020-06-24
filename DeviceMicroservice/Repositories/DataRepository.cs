using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Repositories
{
    public class DataRepository : IDataRepository
    {
        
        private static List<SensorData> mData = new List<SensorData>();
        
        public int korak = 5;
        
        public void PromeniKorak(int k)
        {
            korak = k;
        }
        public int vratiKorak()
        {
            return korak;
        }
        
        public void AddData(SensorData newmData)
        {
            mData.Add(newmData);
        }

        public List<SensorData> GetData()
        {
            return mData;
        }

        public void SetData(List<SensorData> mDataList)
        {
            mData =mDataList;
        }
        
    }
}
