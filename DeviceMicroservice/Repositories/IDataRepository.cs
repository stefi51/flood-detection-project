using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace DeviceMicroservice.Repositories
{
   public interface IDataRepository
    {
        List<SensorData> GetData();
        public void AddData(SensorData mData);
        public void SetData(List<SensorData> mDataList);
        public Dictionary<int, StationParameters> GetStationParameters();
        public void UpdateStationParameter(int stationId, double waterLevel, double waterFlow);
        public void UpdateStationWaterLevel(int stationId, double waterLevel);
        public void UpdateStationWaterFlow(int stationId, double waterFlow);
        public StationParameters GetStationParameters(int stationId);

    }
}
