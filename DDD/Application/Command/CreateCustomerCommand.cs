namespace Customers.Application.Command
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email);
}
