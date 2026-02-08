namespace Customers.Application.CreateCustomer
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email);
}
