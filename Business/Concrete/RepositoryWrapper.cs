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
        public RepositoryWrapper(IOptions<AppSettings> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings;
        }
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IUserRepository _user;
        private IAuthRepository _auth;
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
                if(_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
