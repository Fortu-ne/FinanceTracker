using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface ICategory
    {
        List<Category> GetAll();
        bool createCategory(Category category);
        bool updateCategory(Category category);
        bool findCategory(int Id);
        bool deleteCategory(Category category);

        Category GetCategoryById(int Id);
        bool Save();
    }

}
