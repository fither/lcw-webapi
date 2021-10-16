using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        Task CreateAsync(User user);
        void Update(User user);
        void Delete(User user);
    }
}
