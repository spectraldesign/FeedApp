using System;
using System.Text;
using Domain.Entities;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Application.Messaging
{
    public class RabbitMQClient
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private static readonly string _url = "amqps://rzgnegkz:uHxwhQnXPXWodvQpU6w_1BPKtbBVKb3Z@hawk.rmq.cloudamqp.com/rzgnegkz";

        public RabbitMQClient()
        {

            _factory = new ConnectionFactory()
            {
                Uri = new Uri(_url)
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "polls",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }


        public void PublishNewPoll(Poll p)
        {
            var json = JsonSerializer.Serialize(p);
            var body = Encoding.UTF8.GetBytes(json);
            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.MessageId = "poll_created";
            _channel.BasicPublish(exchange: "",
                routingKey: "polls",
                basicProperties: basicProperties,
                body: body);

            Console.WriteLine(" [x] Sent {0}", Convert.ToString(json));
        }

        public void PublishClosedPoll(PollResult p)
        {
            //Console.WriteLine("Poll", p);
            var json = JsonSerializer.Serialize(p);
            var body = Encoding.UTF8.GetBytes(json);

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.MessageId = "poll_closed";

            _channel.BasicPublish(exchange: "",
                routingKey: "polls",
                basicProperties: basicProperties, 
                body: body);
            Console.WriteLine(" [x] Sent {0}", Convert.ToString(json));
        }
    }
}