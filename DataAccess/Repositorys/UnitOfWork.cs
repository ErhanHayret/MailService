using Data.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }
        public async Task<bool> Save()
        {
            int tmp = await _context.SaveChanges();
            return tmp > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
