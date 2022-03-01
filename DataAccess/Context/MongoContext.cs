using Data.Intefaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class MongoContext:IMongoContext
    {
        private IMongoDatabase _database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        public MongoContext()
        {
            _commands = new List<Func<Task>>();
        }

        public void AddCommand(Func<Task> command)
        {
            _commands.Add(command);
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();
            using(Session =await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();
                var commandTasks = _commands.Select(c => c());
                await Task.WhenAll(commandTasks);
                await Session.CommitTransactionAsync();
            }
            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();
            return _database.GetCollection<T>(name);
        }
        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }
            MongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
            _database = MongoClient.GetDatabase("MailDb");
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
