using System;
using CsvHelper.Configuration;
using DeviceMicroservice.Models;
using Newtonsoft.Json;

namespace DeviceMicroservice.Mappers
{
    public sealed class DataMap : ClassMap<Data>
    {
        public DataMap()
        {
            Map(x => x.Id).ConvertUsing(row => (int) row.GetField<float>("light"));
            //Map(x => x.FirstName).Name("fertilizer_level");
            Map(x => x.K).Name("light");
        }
    }
    
}