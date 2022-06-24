using Forward.Models.model;

namespace Forward.Repository.ROrderHeader
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
