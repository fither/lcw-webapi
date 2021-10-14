using Entities.Models;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IAuthRepository
    {
        User Authenticate(AuthDto auth);
    }
}
