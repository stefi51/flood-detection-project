using System;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using SharedModels;

namespace AnalyticsMicroservice.AServices
{
    public class AnalyticsService
    {
        private IRefinedDataRepository refinedDataRepository;
        
        public AnalyticsService(IRefinedDataRepository refinedDataRepository)
        {
            this.refinedDataRepository = refinedDataRepository;
        }
        
        public void ProcessNewData(SensorData newSData)
        {
            if (newSData.Rainfall > 0.1)
            {
                RefinedData newRefinedData= new RefinedData(newSData.WaterFlow,newSData.WaterLevel,
                    newSData.Rainfall,newSData.StationId,newSData.MeasuredDateTime,DateTime.Now, EventType.Warning);
                this.refinedDataRepository.InsertData(newRefinedData);
            } 
            
        }
    }
}