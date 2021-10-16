using DataAccess.Abstract;

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
