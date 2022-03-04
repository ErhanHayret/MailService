
using ConsumerBusiniess.MailBusiness;
using Data.Models;
using Newtonsoft.Json;
using ProducerBusiniess.RabbitMQBusiniess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MailConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Delay(20000).Wait();
            Mail mail = new Mail();
            //RabbitMQProduce RMQ = RabbitMQProduce.Instance;
            EventingBasicConsumer consumer = new EventingBasicConsumer(RabbitMQProduce.model);
            consumer.Received += (model, ea) =>
            {
                ConsumerSendMailBusiness SendMail = new ConsumerSendMailBusiness();
                ConsumerMongoDbBusiness SetMail = new ConsumerMongoDbBusiness();
                string msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                mail = JsonConvert.DeserializeObject<Mail>(msg);
                SendMail.SendMail(mail);
                SetMail.SetMail(mail);
            };
            RabbitMQProduce.model.BasicConsume(queue: "MailQueue", autoAck: true, consumer: consumer);
            Console.ReadLine();
        }
    }
}
