using Customers.Repository;
using Shared.Model;

namespace Customers.Application.DeleteCustomer
{
    public class DeleteCustomerHandler
    {
        private readonly ICustomerRepository _repo;

        public DeleteCustomerHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(DeleteCustomerCommand cmd)
        {
            var customer = await _repo.GetByIdAsync(cmd.Id);
            if (customer == null)
                return Result.Fail("Customer not found");

            await _repo.DeleteAsync(customer);

            return Result.Ok();
        }
    }
}
