using Forward.Data;
using Forward.Models.model;

namespace Forward.Repository.RCategory
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryDb = _context.Category.FirstOrDefault(x => x.Id == category.Id);
            if(categoryDb != null)
            {
                categoryDb.Name = category.Name;
            }
        }
    }
}
