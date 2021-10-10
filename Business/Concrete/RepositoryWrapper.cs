using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace Business.Concrete
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
        public RepositoryWrapper(DataContext context)
        {
            _context = context;
        }
        private IProductRepository _product;
        private ICategoryRepository _category;
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
