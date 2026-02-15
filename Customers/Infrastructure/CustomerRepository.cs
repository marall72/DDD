using Customers.Application.GetAllCustomers;
using Customers.Data;
using Customers.Entity;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObject;
using System.Net.Http.Headers;

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
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id);
            if (existingCustomer == null)
            {
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

        public async Task<List<Customer>> GetAllAsync(GetAllCustomersQuery criteria)
        {
            var result = _context.Customers.AsQueryable();

            #region Ids
            if (criteria.Ids != null && criteria.Ids.Value != null && criteria.Ids.Value.Any())
                switch (criteria.Ids.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => criteria.Ids.Value.Contains(x.Id));
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => !criteria.Ids.Value.Contains(x.Id));
                        break;
                    default:
                        break;
                }
            #endregion


            #region Firstname

            if (criteria.FirstName != null && !string.IsNullOrEmpty(criteria.FirstName.Value))
                switch (criteria.FirstName.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Firstname == criteria.FirstName.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Firstname != criteria.FirstName.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Firstname.Contains(criteria.FirstName.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Firstname.StartsWith(criteria.FirstName.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Firstname.EndsWith(criteria.FirstName.Value));
                        break;
                    default:
                        break;
                }

            #endregion

            #region Lastname
            if (criteria.LastName != null && !string.IsNullOrEmpty(criteria.LastName.Value))
                switch (criteria.LastName.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Lastname == criteria.LastName.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Lastname != criteria.LastName.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Lastname.Contains(criteria.LastName.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Lastname.StartsWith(criteria.LastName.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Lastname.EndsWith(criteria.LastName.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            #region Email
            if (criteria.Email != null && !string.IsNullOrEmpty(criteria.Email.Value))
                switch (criteria.Email.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Email.Value == criteria.Email.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Email.Value != criteria.Email.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Email.Value.Contains(criteria.Email.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Email.Value.StartsWith(criteria.Email.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Email.Value.EndsWith(criteria.Email.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            if (!string.IsNullOrEmpty(criteria.SearchText))
                result = result.Where(x => x.Firstname.Contains(criteria.SearchText) || x.Lastname.Contains(criteria.SearchText) || x.Email.Value.Contains(criteria.SearchText));

            return await result.Skip(criteria.Offset).Take(criteria.TopCount).ToListAsync();
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
