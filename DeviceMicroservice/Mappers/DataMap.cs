using System;
using CsvHelper.Configuration;
using DeviceMicroservice.Models;
using Newtonsoft.Json;
using SharedModels;

namespace DeviceMicroservice.Mappers
{
    public sealed class DataMap : ClassMap<SensorData>
    {
        public DataMap()
        {
            Map(x => x.Rainfall).Name("rainfall");
            Map(x => x.WaterFlow).Name("water level");
            Map(x => x.WaterLevel).Name("flow water");
            Map(x => x.StationId).ConvertUsing(row => (int) row.GetField<float>("stationId"));
            Map(x => x.MeasuredDateTime).Name("dateTime");

        }
    }
    
}