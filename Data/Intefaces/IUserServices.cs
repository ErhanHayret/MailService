using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intefaces
{
    public interface IUserServices
    {
        User Authenticate(string userName, string password);
        User GetById(string id);
        Task<List<User>> GetUsers();
    }
}
