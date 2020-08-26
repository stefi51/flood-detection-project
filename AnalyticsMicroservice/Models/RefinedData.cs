using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Models
{
    public enum EventType
    {
        Warning,
        Alarm
    }
    public class RefinedData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //public int Value { get; set; }
       // public  int Id2 { get; set; }
        
        public double WaterFlow { get; set; }
        public double WaterLevel { get; set; }
        public double Rainfall { get; set; }
        public int StationId { get; set; }
        public DateTime MeasuredDateTime { get; set; }
        public DateTime AnalyzedDataTime { get; set; }
        public EventType AnalyzedEventType { get; set; }
		public RefinedData() {}
        public RefinedData(double waterFlow, double waterLevel, double rainfall, int stationId, DateTime measuredDateTime, DateTime analyzedDataTime, EventType analyzedEventType)
        {
            WaterFlow = waterFlow;
            WaterLevel = waterLevel;
            Rainfall = rainfall;
            StationId = stationId;
            MeasuredDateTime = measuredDateTime;
            AnalyzedDataTime = analyzedDataTime;
            AnalyzedEventType = analyzedEventType;
        }
    }
}
