using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using DeviceMicroservice.Mappers;
using DeviceMicroservice.Models;

namespace DeviceMicroservice.Services
{
    public class ReadService
    {
        public List<SensorData> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csv.Configuration.RegisterClassMap<DataMap>();
                    var records = csv.GetRecords<SensorData>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}