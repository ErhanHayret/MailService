using Data.Intefaces;
using Data.Models;
using DataAccess.Context;
using DataAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerBusiniess.ProducerBusiness
{
    public class MongoBusiness
    {
        public async void AddUser(User user)
        {
            IMongoContext context = new MongoContext();
            IRepository<User> _repository = new Repository<User>(context);
            UnitOfWork UoW = new UnitOfWork(context);
            _repository.Add(user);
            await UoW.Save();
        }
        public User GetUserById(string id)
        {
            IMongoContext context = new MongoContext();
            IRepository<User> _repository = new Repository<User>(context);
            return _repository.Get(id).Result;
        }
    }
}
