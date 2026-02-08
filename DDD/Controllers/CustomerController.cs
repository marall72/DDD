using Customers.Application.CreateCustomer;
using Customers.Application.DeleteCustomer;
using Customers.Application.GetAllCustomers;
using Customers.Application.GetCustomerById;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerHandler _createHandler;
        private readonly DeleteCustomerHandler _deleteHandler;
        private readonly GetCustomerByIdHandler _getByIdHandler;
        private readonly GetAllCustomersHandler _getAllHandler;

        public CustomerController(
            CreateCustomerHandler createHandler,
            DeleteCustomerHandler deleteHandler,
            GetCustomerByIdHandler getByIdHandler,
            GetAllCustomersHandler getAllHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand cmd)
        {
            var result = await _createHandler.Handle(cmd);

            if (result.IsSuccess)
                return Ok(new { success = true, customerId = result.Value });
            else
                return BadRequest(new { success = false, error = result.Error });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllCustomersQuery(0, 0));

            if (result != null && result.Any())
                return Ok(new { success = true, count = result.Count, data = result });
            else
                return NotFound(new { success = false, message = "No customers found" });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _getByIdHandler.Handle(new GetCustomerByIdQuery(id));

            if (customer == null)
                return NotFound(new { success = false, message = "Customer not found" });

            return Ok(new { success = true, data = customer });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _deleteHandler.Handle(new DeleteCustomerCommand(id));

            if (!result.IsSuccess)
                return BadRequest(new { success = false, error = result.Error });

            return Ok(new { success = true, message = "Customer deleted successfully", customerId = id });
        }
    }
}
