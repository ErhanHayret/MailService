using Data.Intefaces;
using Data.Models;
using DataAccess.Context;
using DataAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerBusiniess.MailBusiness
{
    public class ConsumerMongoDbBusiness
    {
        public async void SetMail(Mail mail)
        {
            IMongoContext context = new MongoContext();
            IRepository<MailDto> _repository = new Repository<MailDto>(context);
            UnitOfWork UoW = new UnitOfWork(context);
            MailDto mailDto = new MailDto() { ArriveEmail = mail.ArriveEmail, SenderEmail = mail.SenderEmail, MailSubject = mail.MailSubject, MailText = mail.MailText };
            _repository.Add(mailDto);
            await UoW.Save();
        }
    }
}
