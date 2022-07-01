using CategoryService.Contract;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CategoryService.AsyncConnection
{
    public class NewsServiceNotification : INotification
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public NewsServiceNotification()
        {
            _connection = new ConnectionFactory() {HostName= "rabbitmq-clusterip-srv", Port=5672 }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "notify", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += _connection_ConnectionShutdown;
            
        }

        private void _connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection is shutdown....");
        }

        public async Task CreateNotify(NewsCategoryCreate newsCategoryCreate)
        {
            var message = JsonSerializer.Serialize(newsCategoryCreate);

            if (_connection.IsOpen)
            {
                Console.WriteLine("message is sending by rabbitmq...");
                
                var body=Encoding.UTF8.GetBytes(message);
                
                _channel.BasicPublish("notify", "",null,body);

                Console.WriteLine($"--> We have sent {message}");
            }
            else
            {
                Console.WriteLine("RabbitMQ Connection is close...!");
            }
        }
        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

    }
}
