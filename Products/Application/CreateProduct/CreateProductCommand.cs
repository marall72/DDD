using Shared.ValueObject;

namespace Products.Application.CreateProduct
{
    public record CreateProductCommand(string Title, decimal priceAmount, string priceCurrency, string Description, int Quantity);
}
