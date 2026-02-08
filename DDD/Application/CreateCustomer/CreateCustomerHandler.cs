using Shared.Entity;
using Customers.Repository;
using Shared.Model;

namespace Customers.Application.CreateCustomer
{
    public class CreateCustomerHandler
    {
        private readonly ICustomerRepository _repo;

        public CreateCustomerHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(CreateCustomerCommand cmd)
        {

            if (!IsValidEmail(cmd.Email))
                return Result<Guid>.Fail("Invalid email format");

            if (await _repo.ExistsByEmailAsync(cmd.Email))
                return Result<Guid>.Fail("Email already in use");

            var id = Guid.NewGuid();
            var customer = Customer.Create(id, cmd.FirstName, cmd.LastName, cmd.Email);
            await _repo.AddAsync(customer);

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
