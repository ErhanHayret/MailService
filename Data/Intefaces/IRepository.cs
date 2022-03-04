using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intefaces
{
    public interface IRepository<T>:IDisposable where T:class
    {
        Task<T> Get(string id);
        Task<List<T>> GetAll();
        void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
    }
}
