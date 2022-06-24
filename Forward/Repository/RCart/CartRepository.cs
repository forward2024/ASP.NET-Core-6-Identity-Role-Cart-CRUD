using Forward.Data;
using Forward.Models.model;

namespace Forward.Repository.RCart
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Decrement(Cart cartItem, int count)
        {
            cartItem.Count -= count;
        }
        public void Increment(Cart cartItem, int count)
        {
            cartItem.Count += count;
        }
    }
}
