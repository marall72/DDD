using Products.Entity;
using Products.Infrastructure;
using Shared.Model;

namespace Products.Application.CreateProduct
{
    public class CreateProductHandler
    {
        private readonly IProductRepository _repo;

        public CreateProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand cmd)
        {

            if (string.IsNullOrEmpty(cmd.Title))
                return Result<Guid>.Fail("Title is required");

            if (string.IsNullOrEmpty(cmd.priceCurrency))
                return Result<Guid>.Fail("Price is required");

            var id = Guid.NewGuid();
            var product = Product.Create(id, cmd.Title, cmd.priceAmount, cmd.priceCurrency, cmd.Description, cmd.Quantity);
            await _repo.AddAsync(product);

            return Result<Guid>.Ok(product.Id);
        }
    }
}
