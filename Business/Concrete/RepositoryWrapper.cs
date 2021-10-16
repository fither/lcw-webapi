using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.Options;

namespace Business.Concrete
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IOptions<MailSettings> _mailSettings;
        public RepositoryWrapper(IOptions<AppSettings> appSettings, IOptions<MailSettings> mailSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings;
            _mailSettings = mailSettings;
        }
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IUserRepository _user;
        private IAuthRepository _auth;
        private IMailService _mailService;
        public IProductRepository Product
        {
            get
            {
                if(_product == null) {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if(_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_appSettings, _context);
                }
                return _auth;
            }
        }

        public IUserRepository User
        {
            get
            {
                if(_mailService == null)
                {
                    _mailService = new MailService(_mailSettings);
                }
                if(_user == null)
                {
                    _user = new UserRepository(_context, _mailService);
                }
                return _user;
            }
        }

        public IMailService MailService
        {
            get
            {
                if(_mailService == null)
                {
                    _mailService = new MailService(_mailSettings);
                }
                return _mailService;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
