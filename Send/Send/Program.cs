using System;
using RabbitMQ.Client;
using System.Text;

namespace Send // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Hello",
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                
                
                for (int i = 0; i < 20; i++)
                {
                    var message = $"Message number {i}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

                    Console.WriteLine("[x] Sent {0}", message);
                }          

           
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}