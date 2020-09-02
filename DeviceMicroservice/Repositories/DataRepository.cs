using DeviceMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace DeviceMicroservice.Repositories
{
    public class DataRepository : IDataRepository
    {

        private static List<SensorData> mData = new List<SensorData>();
        private static Dictionary<int, StationParameters> stationParameters = new Dictionary<int, StationParameters>();

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
            var stationsDictionary = mDataList.GroupBy(o => o.StationId)
                .Select(x => new KeyValuePair<int, StationParameters>(x.Key,
                    new StationParameters() { CommandWaterFlow = 0.0, CommandWaterLevel = 0.0 }))
                .ToDictionary(x => x.Key, x => x.Value);
            SetStationParameters(stationsDictionary);
            mData = mDataList;
        }

        public Dictionary<int, StationParameters> GetStationParameters()
        {
            return stationParameters;
        }

        private void SetStationParameters(Dictionary<int, StationParameters> lDictionary)
        {
            stationParameters = lDictionary;
        }

        public void UpdateStationParameter(int stationId, double waterLevel, double waterFlow)
        {
            stationParameters[stationId].CommandWaterFlow = waterFlow;
            stationParameters[stationId].CommandWaterLevel = waterLevel;
        }

        public void UpdateStationWaterLevel(int stationId, double waterLevel)
        {
            stationParameters[stationId].CommandWaterLevel += waterLevel;
        }

        public void UpdateStationWaterFlow(int stationId, double waterFlow)
        {
            stationParameters[stationId].CommandWaterFlow+= waterFlow;
        }

        public StationParameters GetStationParameters(int stationId)
        {
            return stationParameters[stationId];
        }
    }
}
