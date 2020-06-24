using DeviceMicroservice.Models;
using DeviceMicroservice.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeviceMicroservice.DataPublisher;
using DeviceMicroservice.Services;

namespace DeviceMicroservice
{
    public class Sensors :BackgroundService
    {
        private IDataRepository sensorDataRepo;
        private IDataPublisher sensorDataPublisher;
        private ReadService readService;
      private CancellationTokenSource csource;
        private bool turnedOn;
        private int timeStep;
        private LiveMetaData currentData;
        public int korak { get; set; }
        
        public Sensors(IDataPublisher dataPublisher,IDataRepository iDataRepo,ReadService rdS)
        {
            sensorDataRepo = iDataRepo;
            sensorDataPublisher= dataPublisher;
            readService = rdS;
            sensorDataRepo.SetData(readService.ReadCSVFile("./dataset.csv"));
            turnedOn = true;
            timeStep = 10;
            currentData= new LiveMetaData(1, timeStep, DateTime.Now);
            csource=new CancellationTokenSource();
           // currentData.TimeStep = timeStep;
           // currentData.StationId = 1;
           // currentData.Threshold= DateTime.Now;
        }
        /*public void setKorak(int k)
        {
            this.timeStep = k;
        }*/

        public void ChangeTimeStep(int newTimeStep)
        {
           
            timeStep = newTimeStep;
            csource.Cancel();
            
        }
        

        public LiveMetaData GetMetaData()
        {
            return currentData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (turnedOn)
            {
                
                foreach (var sensorData in sensorDataRepo.GetData())
                {
               
                    try
                    {
                        currentData.TimeStep = timeStep;
                        currentData.StationId = sensorData.StationId;
                        currentData.Threshold = DateTime.Now.Add(new System.TimeSpan(0, 0, 0, timeStep));
                        sensorDataPublisher.SendData(timeStep);
                        //dodati rest zahtev, za slanje
                        await Task.Delay(TimeSpan.FromSeconds(timeStep), csource.Token);
                    }
                    catch (Exception e)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(timeStep), stoppingToken);
                    }
                    
                }

            }
            
        }

    
    }
}
