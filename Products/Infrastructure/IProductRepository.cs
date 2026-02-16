using Products.Entity;

namespace Products.Infrastructure
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<List<Product>> GetAllAsync(Application.GetAllProducts.GetAllProductsQuery query);
        Task<Product?> GetByIdAsync(Guid id);
        Task DeleteAsync(Product product);
    }
}
