using Forward.Models.model;

namespace Forward.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
