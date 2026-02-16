using Products.Entity;
using Products.Infrastructure;

namespace Products.Application.GetAllProducts
{
    public class GetAllProductsHandler
    {
        private readonly IProductRepository _repo;

        public GetAllProductsHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        //TODO: add paging and sorting
        public async Task<List<Product>> Handle(GetAllProductsQuery query)
        {
            return await _repo.GetAllAsync(query);
        }
    }
}
