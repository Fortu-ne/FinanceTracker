using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinanceTracker.Repository
{
    public class CategoryReposiroty : ICategory
    {

        private readonly DataContext _context;

        public CategoryReposiroty(DataContext context)
        {
            _context = context;
        }

        public bool createCategory(Category category)
        {
           _context.Categories.Add(category);
            return Save();
        }

        public bool deleteCategory(Category category)
        {
           var model = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (model != null)
            {
                _context.Categories.Remove(model);
            }

            return Save();
        }

        public bool findCategory(int Id)
        {
            return _context.Categories.Any(c => c.Id == Id);
        }

        public ICollection<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int Id)
        {
            return _context.Categories.Where(r => r.Id == Id).FirstOrDefault();
        }

        public bool Save()
        {
            var saving = _context.SaveChanges();
            return saving > 0 ? true : false;
        }

        public bool updateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
