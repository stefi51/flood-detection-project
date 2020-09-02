using DeviceMicroservice.Models;
using DeviceMicroservice.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceMicroservice.DataPublisher;
using DeviceMicroservice.Services;
using Newtonsoft.Json;
using SharedModels;

namespace DeviceMicroservice
{
    public class Sensors : BackgroundService
    {
        private IDataRepository sensorDataRepo;
        private IDataPublisher sensorDataPublisher;
        private ReadService readService;
        private CancellationTokenSource cancelTokenSource;
        private bool turnedOn;
        private int timeStep;
        private LiveMetaData currentData;
        private static HttpClient _httpClient;
        public Sensors(IDataPublisher dataPublisher, IDataRepository iDataRepo, ReadService rdS)
        {
            sensorDataRepo = iDataRepo;
            sensorDataPublisher = dataPublisher;
            readService = rdS;
            sensorDataRepo.SetData(readService.ReadCSVFile("./Data.csv"));
            turnedOn = true;
            timeStep = 4;
            currentData = new LiveMetaData(sensorDataRepo.GetData().First().StationId, timeStep, DateTime.Now);
            cancelTokenSource = new CancellationTokenSource();
            _httpClient = new HttpClient();
        }

        public void ChangeTimeStep(int newTimeStep)
        {
            timeStep = newTimeStep;
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
            cancelTokenSource = new CancellationTokenSource();
        }


        public LiveMetaData GetMetaData()
        {
            return currentData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cancelToken = new CancellationToken();
            while (turnedOn)
            {
                foreach (var sensorData in sensorDataRepo.GetData())
                {
                    cancelToken = stoppingToken.IsCancellationRequested ? stoppingToken : cancelTokenSource.Token;

                    try
                    {
                        currentData.TimeStep = timeStep;
                        currentData.StationId = sensorData.StationId;
                        currentData.Threshold = DateTime.Now.Add(new System.TimeSpan(0, 0, 0, timeStep));
                        SensorData changedSensorData= ChangeSensorData(sensorData);
                        //ovo je radi testa
                        currentData.waterflow = changedSensorData.WaterFlow;
                        currentData.waterlevel = changedSensorData.WaterLevel;
                        //
                        sensorDataPublisher.SendData(changedSensorData);
                       // SendRestData(changedSensorData);
                        await Task.Delay(TimeSpan.FromSeconds(timeStep), cancelToken);
                    }
                    catch (Exception e)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                    }
                }
            }
        }

        private SensorData ChangeSensorData(SensorData data)
        {
            var dStationParameters = sensorDataRepo.GetStationParameters(data.StationId);
            SensorData newData = new SensorData()
            {
                MeasuredDateTime = data.MeasuredDateTime,
                Rainfall = data.Rainfall,
                StationId = data.StationId,
                WaterFlow = data.WaterFlow,
                WaterLevel = data.WaterLevel
            };
            newData.WaterFlow += dStationParameters.CommandWaterFlow;
            newData.WaterLevel += dStationParameters.CommandWaterLevel;
            if (newData.WaterLevel < 0.0)
            {
                newData.WaterLevel = 0.0;

            }
            if (newData.WaterFlow < 0.0)
            {
                newData.WaterFlow = 0.0;
            }

            return newData;
        }
        protected async void SendRestData(SensorData payload)
        {
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://datamicroservice:3000/data", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}