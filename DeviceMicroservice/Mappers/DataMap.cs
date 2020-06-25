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
          //  Map(x => x.Id).ConvertUsing(row => (int) row.GetField<float>("light"));
            //Map(x => x.FirstName).Name("fertilizer_level");
            //Map(x => x.K).Name("light");
            Map(x => x.Rainfall).Name("fertilizer_level");
            Map(x => x.WaterFlow).Name("light");
            Map(x => x.WaterLevel).Name("soil_moisture_percent");
            Map(x => x.StationId).ConvertUsing(row => (int) row.GetField<float>("light"));
            Map(x => x.MeasuredDateTime).Name("capture_datetime_utc");

        }
    }
    
}