using System.Collections.Generic;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
