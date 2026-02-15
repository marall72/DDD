using Customers.Application.CreateCustomer;
using Customers.Entity;
using Customers.Infrastructure;
using Shared.Model;

namespace Customers.Application.UpdateCustomer
{
    public class UpdateCustomerHandler
    {
        private readonly ICustomerRepository _repo;

        public UpdateCustomerHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(UpdateCustomerCommand cmd)
        {

            if (!IsValidEmail(cmd.Email))
                return Result<Guid>.Fail("Invalid email format");

            if (await _repo.ExistsByEmailAsync(cmd.Email))
                return Result<Guid>.Fail("Email already in use");

            var customer = Customer.Create(cmd.Id, cmd.FirstName, cmd.LastName, cmd.Email);
            await _repo.UpdateAsync(customer);

            return Result<Guid>.Ok(customer.Id);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
