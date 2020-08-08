using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using SharedModels;

namespace DeviceMicroservice.CommandReceiver
{
    public class CommandReceiver: BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;
        private readonly Sensors _sensorsService;

        public CommandReceiver(Sensors sensorsService, IOptions<RabbitMQConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
            _sensorsService = sensorsService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                Port = _port
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var newCommand =
                    JsonConvert.DeserializeObject<ICommand>(content, new CommandConverter(_sensorsService));
                // HandleMessage(updateCustomerFullNameModel);
               // System.Diagnostics.Debug.WriteLine(updateCustomerFullNameModel.Naziv);
               newCommand.Run();
               //HandleMessage(10);
               

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
           
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
          
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
         
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            
        }

        private void HandleMessage(int updateCustomerFullNameModel)
        {
           // _sensorsService.setKorak(updateCustomerFullNameModel);
           _sensorsService.ChangeTimeStep(updateCustomerFullNameModel);
        }
    }
}
