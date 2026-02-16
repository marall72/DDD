using Microsoft.EntityFrameworkCore;
using Products.Application.GetAllProducts;
using Products.Data;
using Products.Entity;

namespace Products.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync(GetAllProductsQuery criteria)
        {
            var result = _context.Products.AsQueryable();

            if(criteria.Ids != null && criteria.Ids.Value != null && criteria.Ids.Value.Length > 0)
            {
                switch (criteria.Ids.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(p => criteria.Ids.Value.Contains(p.Id));
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(p => !criteria.Ids.Value.Contains(p.Id));
                        break;
                    default:
                        break;
                }
            }

            if(criteria.Title != null && !string.IsNullOrEmpty(criteria.Title.Value))
            {
                switch (criteria.Title.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(p => p.Title == criteria.Title.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(p => p.Title != criteria.Title.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(p => p.Title.Contains(criteria.Title.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(p => p.Title.StartsWith(criteria.Title.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(p => p.Title.EndsWith(criteria.Title.Value));
                        break;
                    default:
                        break;
                }
            }

            if(criteria.Description != null && !string.IsNullOrEmpty(criteria.Description.Value))
            {
                switch (criteria.Description.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(p => p.Description == criteria.Description.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(p => p.Description != criteria.Description.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(p => p.Description.Contains(criteria.Description.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(p => p.Description.StartsWith(criteria.Description.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(p => p.Description.EndsWith(criteria.Description.Value));
                        break;
                    default:
                        break;
                }
            }

            if(criteria.Quantity != null)
            {
                switch (criteria.Quantity.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(p => p.Quantity == criteria.Quantity.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(p => p.Quantity != criteria.Quantity.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(p => p.Quantity > criteria.Quantity.Value);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(p => p.Quantity < criteria.Quantity.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(p => p.Quantity >= criteria.Quantity.Value);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(p => p.Quantity <= criteria.Quantity.Value);
                        break;
                    default:
                        break;
                }
            }

            if(criteria.Price != null && criteria.Price.Value != null)
            {
                switch (criteria.Price.Operator)
                {
                    //TODO: test price filter with different operators
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x=> x.Price == criteria.Price.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Price != criteria.Price.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Price.Currency.Contains(criteria.Price.Value.Currency));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Price.Currency.StartsWith(criteria.Price.Value.Currency));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Price.Currency.EndsWith(criteria.Price.Value.Currency));
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.Price.Amount > criteria.Price.Value.Amount);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.Price.Amount < criteria.Price.Value.Amount);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.Price.Amount >= criteria.Price.Value.Amount);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.Price.Amount <= criteria.Price.Value.Amount);
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(criteria.SearchText))
                result = result.Where(x => x.Title.Contains(criteria.SearchText) || x.Description.Contains(criteria.SearchText) || x.Price.Currency.Contains(criteria.SearchText));

            return await result.Skip(criteria.Offset).Take(criteria.TopCount).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
