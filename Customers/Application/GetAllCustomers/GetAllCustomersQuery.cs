using Shared.Model;

namespace Customers.Application.GetAllCustomers
{
    public record GetAllCustomersQuery : BaseFilterCriteria
    {
        public FilterField<Guid[]>? Ids { get; set; }
        public string? SearchText { get; set; }
        public FilterField<string>? FirstName { get; set; }
        public FilterField<string>? LastName { get; set; }
        public FilterField<string>? Email { get; set; }
    }
}
