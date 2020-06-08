using DeviceMicroservice.Models;
using DeviceMicroservice.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceMicroservice
{
    public class Sensors :BackgroundService
    {
        private IDataRepository d;
        public int korak { get; set; }
        public Sensors(IDataRepository id)
        {
            d = id;
            korak = 5;
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
