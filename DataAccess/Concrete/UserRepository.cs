using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id.Equals(id));
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.EmailAddress.Equals(email));
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
