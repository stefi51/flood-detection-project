using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMicroservice.CommandReceiver
{
    public class RabbitMQConfiguration
    {
        public string Hostname { get; set; }

        public string QueueName { get; set; }
        public string QueueSensorName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public int Port { get; set; }
    }
}
