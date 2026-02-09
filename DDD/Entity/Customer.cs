using Shared.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.Entity
{
    public class Customer
    {
        public Customer()
        {
            
        }

        public Customer(Guid id, string firstName, string lastName, Email email)
        {
            Id = id;
            Firstname = firstName;
            Lastname = lastName;
            Email = email;
        }

        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public Email Email { get; set; }

        public static Customer Create(Guid id, string firstName, string lastName, string email)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));

            if(firstName.Length > 255)
                throw new ArgumentException("First name cannot exceed 255 characters.", nameof(firstName));

            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));

            if(lastName.Length > 255)
                throw new ArgumentException("Last name cannot exceed 255 characters.", nameof(lastName));

            return new Customer(id, firstName, lastName, new Email(email));
        }
    }
}
