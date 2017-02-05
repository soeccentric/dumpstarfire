using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApplication1.Services
{
    public interface IMessageSource
    {
        void Shutdown();
        void Initialize();
        IConnectableObservable<string> Messages();
    }

    public class MessageSource : IMessageSource
    {
        private IConnection _connection;
        private IModel _channel;
        private IConnectableObservable<string> _source;

        public MessageSource()
        {
          
        }

        public IConnectableObservable<string> Messages()
        {
            return _source;
        }

        public void Shutdown()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public void Initialize()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            _source = Observable.FromEventPattern<BasicDeliverEventArgs>(consumer, "Received")
                .Select(em =>
                {
                    var body = em.EventArgs.Body;
                    return Encoding.UTF8.GetString(body);
                })
                .Do(message => Debug.WriteLine(message))
                .Publish();

            _channel.BasicConsume(queue: "hello",
                noAck: true,
                consumer: consumer);
        }
    }
}