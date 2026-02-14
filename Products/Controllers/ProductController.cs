using Products.Application.CreateProduct;
using Products.Application.DeleteProduct;
using Products.Application.GetAllProducts;
using Products.Application.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductHandler _createHandler;
        private readonly DeleteProductHandler _deleteHandler;
        private readonly GetProductByIdHandler _getByIdHandler;
        private readonly GetAllProductsHandler _getAllHandler;

        public ProductController(
            CreateProductHandler createHandler,
            DeleteProductHandler deleteHandler,
            GetProductByIdHandler getByIdHandler,
            GetAllProductsHandler getAllHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd)
        {
            var result = await _createHandler.Handle(cmd);

            if (result.IsSuccess)
                return Ok(new { success = true, productId = result.Value });
            else
                return BadRequest(new { success = false, error = result.Error });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllProductsQuery(0, 0));

            if (result != null && result.Any())
                return Ok(new { success = true, count = result.Count, data = result });
            else
                return NotFound(new { success = false, message = "No products found" });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _getByIdHandler.Handle(new GetProductByIdQuery(id));

            if (customer == null)
                return NotFound(new { success = false, message = "Product not found" });

            return Ok(new { success = true, data = customer });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _deleteHandler.Handle(new DeleteProductCommand(id));

            if (!result.IsSuccess)
                return BadRequest(new { success = false, error = result.Error });

            return Ok(new { success = true, message = "Product deleted successfully", customerId = id });
        }
    }
}
