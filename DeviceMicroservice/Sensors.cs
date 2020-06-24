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
        private IDataRepository d;
        private IDataPublisher _dataPublisher;
        public int korak { get; set; }
        public Sensors(IDataPublisher dataPublisher,IDataRepository id,ReadService rdS)
        {
            d = id;
            korak = 5;
            _dataPublisher = dataPublisher;
            d.SetData(rdS.ReadCSVFile("./dataset.csv"));
        }
        public void setKorak(int k)
        {
            this.korak = k;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                try
                {
                    Data k = new Data()
                    {
                        Id = this.korak,
                        FirstName = "mm2"
                    };
                    d.AddData(k);
                    _dataPublisher.SendData(this.korak);
                    await Task.Delay(TimeSpan.FromSeconds(this.korak), stoppingToken);

                }
                catch (Exception ex)
                {
                    await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
                }
                i++;
            }
        }

    
    }
}
