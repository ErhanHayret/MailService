using Data.Models;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System;

namespace ProducerBusiniess.RabbitMQBusiniess
{
    public class RabbitMQProduce
    {
        public static ConnectionFactory factory;
        public static IConnection conn;
        public static IModel model;
        private static RabbitMQProduce instance = null;
        public static RabbitMQProduce Instance
        {
            get
            {
                //Console.WriteLine(Environment.GetEnvironmentVariable("ELASTIC_ENVIRONMENT"));
                if (instance == null)
                {
                    instance = new RabbitMQProduce();
                    factory = new ConnectionFactory() 
                    { 
                        HostName = "rabbitmq", 
                        UserName = "root", 
                        Password = "root", 
                        Port = 5672 
                    };
                    conn = factory.CreateConnection();
                    model = conn.CreateModel();
                    model.QueueDeclare(queue: "MailQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                }
                return instance;
            }
        }
        public void Set(Mail mail)
        {
            string msg = JsonConvert.SerializeObject(mail);
            var body = Encoding.UTF8.GetBytes(msg);
            model.BasicPublish(exchange: "", routingKey: "MailQueue", basicProperties: null, body: body);
        }
    }
}
