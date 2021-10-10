using DataAccess.Abstract;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetByCategory(string categoryName)
        {
            return _context.Products.Where(product => product.CategoryName.Equals(categoryName)).ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(product => product.Id.Equals(id));
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
