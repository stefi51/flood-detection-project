using System;
using System.Net.Http;
using System.Text;
using AnalyticsMicroservice.Infrastructure;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SharedModels;

namespace AnalyticsMicroservice.AServices
{
    public class AnalyticsService
    {
        private IRefinedDataRepository refinedDataRepository;
        private static HttpClient _httpClient;
        private IHubContext<NotificationService> hub { get; set; }

        public AnalyticsService(IRefinedDataRepository refinedDataRepository, IHubContext<NotificationService> hub)
        {
            this.refinedDataRepository = refinedDataRepository;
            this.hub = hub;
            _httpClient = new HttpClient();
        }
        
        public void ProcessNewData(SensorData newSData)
        {
            if (newSData.Rainfall > 0.1)
            {
                RefinedData newRefinedData= new RefinedData(newSData.WaterFlow,newSData.WaterLevel,
                    newSData.Rainfall,newSData.StationId,newSData.MeasuredDateTime,DateTime.Now, EventType.Warning);
                this.refinedDataRepository.InsertData(newRefinedData);

                this.hub.Clients.All.SendAsync("refinedDataUpdate", newSData);
                // this.hub.Clients.All.SendAsync("refinedDataUpdate", newSData);
                //  this.DecreaseWaterFlow(newSData);
                //this.IncreaseWaterFlow(newSData);
                //  this.IncreaseWaterLevel(newSData);
                // this.DecreaseWaterLevel(newSData);
            }

        }
        protected async void DecreaseWaterLevel(SensorData payload)
        {
            string strPayload = JsonConvert.SerializeObject(new DecreaseWaterLevel()
            {
                Name = "Decrease",
                MinusWaterLevel= 10.5,
                StationId = 10
            });
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://commandmicroservice:80/api/Command/decreasewaterlevel", c);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        protected async void IncreaseWaterLevel(SensorData payload)
        {
            string strPayload = JsonConvert.SerializeObject(new IncreaseWaterLevel()
            {
                Name = "Decrease",
                PlusWaterLevel = 10.5,
                StationId = 10
            });
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://commandmicroservice:80/api/Command/increasewaterlevel", c);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        protected async void IncreaseWaterFlow(SensorData payload)
        {
            string strPayload = JsonConvert.SerializeObject(new IncreaseWaterFlow()
            {
                Name = "Decrease",
                PlusWaterFlow = 10.5,
                StationId = 10
            });
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://commandmicroservice:80/api/Command/increasewaterflow", c);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected async void DecreaseWaterFlow(SensorData payload)
        {
            string strPayload = JsonConvert.SerializeObject(new DecreaseWaterFlow()
            {
                Name = "Decrease",
                MinusWaterFlow = 10.0,
                StationId = 10
            });
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://commandmicroservice:80/api/Command/decreasewaterflow", c);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}