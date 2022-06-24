using Forward.Models.model;

namespace Forward.Data.ViewModel
{
    public class CartVM
    {
        public IEnumerable<Cart> ListOfCart = new List<Cart>();
        public OrderHeader OrderHeader { get; set; }
        public Product Product { get; set; }
    }
}
