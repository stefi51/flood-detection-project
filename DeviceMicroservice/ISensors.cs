using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice
{
    public interface ISensors:IHostedService
    {
        public void setKorak(int k);
    }
}
