using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels
{
    public class SensorData
    {
        /*public int Id { get; set; }
        public string FirstName { get; set; }
        public float K { get; set; }*/
        
        public double WaterFlow { get; set; }
        public double WaterLevel { get; set; }
        public double Rainfall { get; set; }
        public int StationId { get; set; }
        public DateTime MeasuredDateTime { get; set; }
             
    }
}
