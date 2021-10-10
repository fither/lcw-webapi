using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserServiceRepository
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
