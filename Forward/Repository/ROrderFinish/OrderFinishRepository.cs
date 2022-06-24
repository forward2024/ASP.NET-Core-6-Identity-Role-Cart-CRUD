using Forward.Data;
using Forward.Models.model;

namespace Forward.Repository.ROrderFinish
{
    public class OrderFinishRepository : Repository<OrderFinish>, IOrderFinishRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderFinishRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderFinish orderFinish)
        {
            _context.OrderFinishes.Update(orderFinish);
        }
    }
}
