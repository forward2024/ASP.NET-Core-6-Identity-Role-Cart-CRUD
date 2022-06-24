using Forward.Data;
using Forward.Repository.RAppUser;
using Forward.Repository.RCart;
using Forward.Repository.RCategory;
using Forward.Repository.ROrderFinish;
using Forward.Repository.ROrderHeader;
using Forward.Repository.RProductRepository;

namespace Forward.Repository.RUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IAppUserRepository AppUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderFinishRepository OrderFinish { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart = new CartRepository(context);
            AppUser = new AppUserRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
            OrderFinish = new OrderFinishRepository(context);
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
