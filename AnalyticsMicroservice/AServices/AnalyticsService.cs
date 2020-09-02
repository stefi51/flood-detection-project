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
            /*            if (newSData.Rainfall > 0.1)
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

                        }*/
            RefinedData newRefinedData = new RefinedData()
            {
                AnalyzedDataTime = DateTime.Now,
                StationId = newSData.StationId,
                MeasuredDateTime = newSData.MeasuredDateTime,
                Rainfall = newSData.Rainfall,
                WaterFlow = newSData.WaterFlow,
                WaterLevel = newSData.WaterLevel
            };
            if (newRefinedData.Rainfall > 1.4 && newRefinedData.WaterLevel >5.70 && newRefinedData.WaterFlow < 0.80 )
            {
                newRefinedData.AnalyzedEventType = EventType.Alarm;
                this.refinedDataRepository.InsertData(newRefinedData);
                this.hub.Clients.All.SendAsync("refinedDataUpdate", newRefinedData);
                this.DecreaseWaterLevel(newRefinedData.StationId,10);
                this.IncreaseWaterFlow(newRefinedData.StationId,10);
                return;
            }
            else
            {
                if (newRefinedData.WaterLevel > 5.815&& newRefinedData.WaterFlow> 0.8)
                {
                    newRefinedData.AnalyzedEventType = EventType.Warning;
                    this.refinedDataRepository.InsertData(newRefinedData);
                    this.hub.Clients.All.SendAsync("refinedDataUpdate", newRefinedData);
                    this.DecreaseWaterLevel(newRefinedData.StationId, 0.5);
                    this.IncreaseWaterFlow(newRefinedData.StationId, 0.5);
                }
                else if(newRefinedData.WaterFlow<0.6)
                {
                    newRefinedData.AnalyzedEventType = EventType.Warning;
                    this.refinedDataRepository.InsertData(newRefinedData);
                    this.hub.Clients.All.SendAsync("refinedDataUpdate", newRefinedData);
                    this.IncreaseWaterFlow(newRefinedData.StationId, 0.5);
                }
            }
            


        }
        protected async void DecreaseWaterLevel(int stationId, double minusWaterFlow)
        {
            string strPayload = JsonConvert.SerializeObject(new DecreaseWaterLevel()
            {
                Name = "Decrease",
                MinusWaterLevel = minusWaterFlow,
                StationId = stationId
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
        protected async void IncreaseWaterLevel(int stationId, double plusWaterLevel)
        {
            string strPayload = JsonConvert.SerializeObject(new IncreaseWaterLevel()
            {
                Name = "Decrease",
                PlusWaterLevel = plusWaterLevel,
                StationId = stationId
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
        protected async void IncreaseWaterFlow(int stationId, double plusWaterFlow)
        {
            string strPayload = JsonConvert.SerializeObject(new IncreaseWaterFlow()
            {
                Name = "Decrease",
                PlusWaterFlow = plusWaterFlow,
                StationId = stationId
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

        protected async void DecreaseWaterFlow(int stationId, double minusWaterFlow)
        {
            string strPayload = JsonConvert.SerializeObject(new DecreaseWaterFlow()
            {
                Name = "Decrease",
                MinusWaterFlow = minusWaterFlow,
                StationId = stationId
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