using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqExp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(" Press [q] to exit.");
      Console.WriteLine("enter message");

      var factory = new ConnectionFactory() { HostName = "localhost" };

      using (var connection = factory.CreateConnection())
      using (var channel = connection.CreateModel())
      {
        channel.QueueDeclare(queue: "hello",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
        string line;
        do
        {
          //Check for exit conditions
          line = Console.ReadLine();
          if (!string.IsNullOrEmpty(line))
          {
            var body = Encoding.UTF8.GetBytes(line);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($"[x] Sent {line}");
          }

      
        } while (!string.IsNullOrWhiteSpace(line) && line.ToLower() != "q");



      }


    }
  }
}

