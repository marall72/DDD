using Customers.Infrastructure;
using Customers.Entity;

namespace Customers.Application.GetCustomerById
{
    public class GetCustomerByIdHandler
    {
        private readonly ICustomerRepository _repo;

        public GetCustomerByIdHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Customer?> Handle(GetCustomerByIdQuery cmd)
        {
            return await _repo.GetByIdAsync(cmd.Id);
        }
    }
}
