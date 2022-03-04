using Data.Intefaces;
using Data.Models;
using DataAccess.Context;
using DataAccess.Repositorys;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProducerBusiniess.RabbitMQBusiniess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MailProducer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;
        public MailController(ILogger<MailController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetMail")]
        public Task<List<MailDto>> GetMail()
        {
            IMongoContext context = new MongoContext();
            IRepository<MailDto> _repository = new Repository<MailDto>(context);
            return _repository.GetAll();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            IMongoContext context = new MongoContext();
            IRepository<MailDto> _repository = new Repository<MailDto>(context);
            return Ok(_repository.Get(id).Result);
        }

        [HttpPost("SendMail")]
        public IActionResult SendMail(Mail mail)
        {
            RabbitMQProduce RMQ = RabbitMQProduce.Instance;
            RMQ.Set(mail);
            return Ok();
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            try
            {
                _logger.LogInformation("Yasin");
                //throw new Exception("Error!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Test Error!");
            }
            
            return Ok();
        }
    }
}
