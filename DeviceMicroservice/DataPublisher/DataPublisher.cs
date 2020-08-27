using System.Text;
using DeviceMicroservice.CommandReceiver;
using DeviceMicroservice.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedModels;

namespace DeviceMicroservice.DataPublisher
{
    public interface IDataPublisher
    {
        void SendData(SensorData sensorData);
    }
    public class DataPublisher:IDataPublisher
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;

        public DataPublisher(IOptions<RabbitMQConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueSensorName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
            
        }
        
        public void SendData(SensorData sensorData)
        {
            var factory = new ConnectionFactory() { HostName =_hostname, UserName = _username, Password = _password, Port=_port };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(sensorData);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}