using Forward.Models.model;

namespace Forward.Data.ViewModel
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; } = new OrderHeader();
        public IEnumerable<OrderHeader> ListOfOrders = new List<OrderHeader>();
        public IEnumerable<OrderFinish> orderFinishes = new List<OrderFinish>();
    }
}
