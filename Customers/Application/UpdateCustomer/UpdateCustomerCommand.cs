namespace Customers.Application.UpdateCustomer
{
    public record UpdateCustomerCommand(Guid Id, string FirstName, string LastName, string Email);
}
