using Shared.Model;
using Shared.ValueObject;

namespace Products.Application.GetAllProducts
{
    public record GetAllProductsQuery : BaseFilterCriteria
    {
        public FilterField<Guid[]>? Ids { get; set; }
        public FilterField<string>? Title { get; set; }
        public FilterField<Price>? Price { get; set; }
        public FilterField<string>? Description { get; set; }
        public FilterField<int>? Quantity { get; set; }
    }
}
