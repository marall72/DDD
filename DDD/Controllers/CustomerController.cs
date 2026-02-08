using Customers.Application.Command;
using Customers.Application.Handler;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerHandler _createHandler;
        private readonly DeleteCustomerHandler _deleteHandler;

        public CustomerController(
            CreateCustomerHandler createHandler,
            DeleteCustomerHandler deleteHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand cmd)
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
            throw new NotImplementedException("");
            //var result = await _getAllHandler.Handle();

            //if (result != null && result.Any())
            //    return Ok(new { success = true, count = result.Count, data = result });
            //else
            //    return NotFound(new { success = false, message = "No customers found" });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException("");
            //var customer = await _getByIdHandler.Handle(id);

            //if (customer == null)
            //    return NotFound(new { success = false, message = "Customer not found" });

            //return Ok(new { success = true, data = customer });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException("");

            //var cmd = new DeleteCustomerCommand(id);
            //var result = await _deleteHandler.Handle(cmd);

            //if (!result.IsSuccess)
            //    return BadRequest(new { success = false, error = result.Error });

            //return Ok(new { success = true, message = "Customer deleted successfully", customerId = id });
        }
    }
}
