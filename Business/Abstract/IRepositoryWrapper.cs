using DataAccess.Abstract;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IAuthRepository Auth { get; }
        IMailService MailService { get; }

        void Save();
    }
}
