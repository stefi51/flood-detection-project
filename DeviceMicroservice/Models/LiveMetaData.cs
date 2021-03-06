using System;
using System.ComponentModel.Design;

namespace DeviceMicroservice.Models
{
    public class LiveMetaData
    {
        public int StationId { get; set; }
        public int TimeStep {get; set; }
        public DateTime Threshold { get; set; }
        public  double waterlevel { get; set; }
        public  double waterflow { get; set; }
        public  double rainfall { get; set; }
        public LiveMetaData(int stationId, int timeStep, DateTime threshold)
        {
            StationId = stationId;
            TimeStep = timeStep;
            Threshold = threshold;
        }
    }
    
}