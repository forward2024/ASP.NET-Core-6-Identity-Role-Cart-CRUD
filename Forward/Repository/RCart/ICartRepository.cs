using Forward.Models.model;

namespace Forward.Repository.RCart
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Increment(Cart cartItem, int count);
        void Decrement(Cart cartItem, int count);
    }
}
