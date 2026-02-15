using Shared.Model;

namespace Customers.Application.GetAllCustomers
{
    public class GetCustomersFilterCriteria : BaseFilterCriteria
    {
        public Guid[] Ids { get; set; }
        public string SearchText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
