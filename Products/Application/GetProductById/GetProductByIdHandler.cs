using Products.Entity;
using Products.Infrastructure;

namespace Products.Application.GetProductById
{
    public class GetProductByIdHandler
    {
        private readonly IProductRepository _repo;

        public GetProductByIdHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product?> Handle(GetProductByIdQuery cmd)
        {
            return await _repo.GetByIdAsync(cmd.Id);
        }
    }
}
