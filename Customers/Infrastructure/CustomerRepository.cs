using Customers.Application.GetAllCustomers;
using Customers.Data;
using Customers.Entity;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObject;

namespace Customers.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x=> x.Id == customer.Id);
            if (existingCustomer == null) {
                throw new Exception("User does not exist");
            }

            existingCustomer.Firstname = customer.Firstname;
            existingCustomer.Email = customer.Email;
            existingCustomer.Lastname = customer.Lastname;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email.Value == email);
        }

        public async Task<List<Customer>> GetAllAsync(GetCustomersFilterCriteria criteria)
        {
            return await _context.Customers
                .Where(x=> (criteria.Ids == null || criteria.Ids.Contains(x.Id)) &&
                (string.IsNullOrEmpty(criteria.FirstName) || x.Firstname.Contains(criteria.FirstName)) &&
                (string.IsNullOrEmpty(criteria.LastName) || x.Lastname.Contains(criteria.LastName)) &&
                (string.IsNullOrEmpty(criteria.Email) || x.Email.Equals(new Email(criteria.Email)))
                )
                .ToListAsync();
        }
        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
