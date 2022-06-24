using Forward.Repository.RAppUser;
using Forward.Repository.RCart;
using Forward.Repository.ROrderFinish;
using Forward.Repository.ROrderHeader;

namespace Forward.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        IAppUserRepository AppUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderFinishRepository OrderFinish { get; }
        void save();
    }
}
