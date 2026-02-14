using Products.Application.CreateProduct;
using Products.Entity;
using Products.Infrastructure;
using Shared.Model;

namespace Products.Application.DeleteProduct
{
    public class DeleteProductHandler
    {
        private readonly IProductRepository _repo;

        public DeleteProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(DeleteProductCommand cmd)
        {
            var product = await _repo.GetByIdAsync(cmd.Id);
            if (product == null)
                return Result.Fail("Product not found");

            await _repo.DeleteAsync(product);

            return Result.Ok();
        }
    }
}
