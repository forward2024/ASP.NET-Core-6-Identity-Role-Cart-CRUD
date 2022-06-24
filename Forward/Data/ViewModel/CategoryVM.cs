using Forward.Models.model;

namespace Forward.Data.ViewModel
{
    public class CategoryVM
    {
        public Category category { get; set; } = new Category();
        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }
}
