using Customers.Infrastructure;
using Customers.Entity;

namespace Customers.Application.GetAllCustomers
{
    public class GetAllCustomersHandler
    {
        private readonly ICustomerRepository _repo;

        public GetAllCustomersHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        //TODO: add paging and sorting
        public async Task<List<Customer>> Handle(GetAllCustomersQuery query)
        {
            return await _repo.GetAllAsync(query.criteria);
        }
    }
}
