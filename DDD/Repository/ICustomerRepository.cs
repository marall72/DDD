using Shared.Entity;

namespace Customers.Repository
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<bool> ExistsByEmailAsync(string email);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Customer customer);
    }
}
