using Data.Intefaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //MongoClient _client;
        protected readonly IMongoContext _context;
        protected IMongoCollection<T> _dbSet;
        public Repository(IMongoContext context)
        {
            _context = context;
            _dbSet = _context.GetCollection<T>(typeof(T).Name);
        }
        public void Add(T entity)
        {
            _context.AddCommand(() => _dbSet.InsertOneAsync(entity));
        }
        public void Update(T entity)
        {
            _context.AddCommand(() => _dbSet.ReplaceOneAsync(m=>m==entity, entity));
        }
        public void Delete(T entity)
        {
            _context.AddCommand(() => _dbSet.DeleteOneAsync(m => m == entity));
        }
        public Task<T> Get(string id)
        {
            var filter= Builders<T>.Filter.Eq("_id", id);
            var obj = _dbSet.Find(filter).SingleOrDefault();
            return Task.FromResult(obj);
        }
        public async Task<List<T>> GetAll()
        {
            var all = await _dbSet.Find(_=>true).ToListAsync();
            return all.ToList();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
