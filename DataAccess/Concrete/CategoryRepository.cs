using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public void Create(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(category => category.Id.Equals(id));
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
