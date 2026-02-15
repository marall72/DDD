using Customers.Application.GetAllCustomers;
using Customers.Entity;

namespace Customers.Infrastructure
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<bool> ExistsByEmailAsync(string email);
        Task<List<Customer>> GetAllAsync(GetAllCustomersQuery criteria);
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Customer customer);
    }
}
