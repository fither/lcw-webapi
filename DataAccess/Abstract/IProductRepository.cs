using System.Collections.Generic;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetByCategory(string categoryName);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
