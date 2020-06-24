using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.Repositories
{
   public interface IDataRepository
    {
        List<SensorData> GetData();
        public void AddData(SensorData mData);
        public void PromeniKorak(int k);
        public int vratiKorak();
        public void SetData(List<SensorData> mDataList);

    }
}
