using DataLayer.CQRS.Commands.Request;
using DataLayer.CQRS.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetProductList - Endpoint for seeking the Products.
        /// </summary>
        /// <param name="search">(optional) Search text for Product Name</param>
        /// <param name="minPrice">(optional) Filtering the products according to the price</param>
        /// <param name="maxPrice">(optional) Filtering the products according to the price</param>
        /// <response code="200">Product(s) found according to the search text (if exists)</response>
        /// <response code="204">Product(s) not found according to the search text (if exists)</response>
        /// <returns>Array of Product</returns>
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "GetProductList", Description = "Endpoint for seeking the Products.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] GetProductListQueryRequest request)
        {
            var data = await _mediator.Send(request);
            if (data == null || data.Count == 0) return NoContent();
            return Ok(data);
        }

        /// <summary>
        /// GetProductDetails - Endpoint for get a single Product by id.
        /// </summary>
        /// <param name="id">(mandatory) Id of the Product</param>
        /// <returns>Product</returns>
        [HttpGet("{ProductID}")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "GetProductDetails", Description = "Endpoint for get a single Product by id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid ProductID)
        {
            var request = new GetProductByIdQueryRequest() { ProductID = ProductID };
            var data = await _mediator.Send(request);
            if (data == null) return NotFound(new { Message = "Product not found" });
            return Ok(data);
        }

        /// <summary>
        /// AddProduct - Endpoint for add a new Product
        /// </summary>
        /// <param name="request">JSON Request body of the add request</param>
        /// <returns>The added Product with Id</returns>
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "AddProduct", Description = "Endpoint for add a new Product.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// UpdateProduct - Endpoint for updating an existing Product
        /// </summary>
        /// <param name="id">ID value of the Product</param>
        /// <param name="request">JSON Request body of the update request</param>
        /// <returns>Product</returns>
        [HttpPut]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "UpdateProduct", Description = "Endpoint for updating an existing Product.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// DeleteProduct - Endpoint for deleting a Product
        /// </summary>
        /// <param name="id">ID value of the Product</param>
        /// <returns></returns>
        [HttpDelete("{ProductID}")]
        [SwaggerOperation(Summary = "DeleteProduct", Description = "Endpoint for updating an existing Product.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid ProductID)
        {
            var request = new DeleteProductCommandRequest() { ProductID = ProductID };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}