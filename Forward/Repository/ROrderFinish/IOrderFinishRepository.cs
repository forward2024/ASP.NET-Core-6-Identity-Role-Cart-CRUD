using Forward.Models.model;

namespace Forward.Repository.ROrderFinish
{
    public interface IOrderFinishRepository : IRepository<OrderFinish>
    {
        void Update(OrderFinish item);
    }
}
