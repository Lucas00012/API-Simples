using API.Core.Entities;
using API.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Features.Products.Controllers
{
    [ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/products")]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("")]
		[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> GetAll()
		{
			var products = await _mediator.Send(new GetProducts.Query());

			return Ok(products);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Get(int id)
		{
			var product = await _mediator.Send(new GetProduct.Query(id));

			return Ok(product);
		}

		[HttpPost("")]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
		[ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
		public async Task<IActionResult> Add([FromBody]AddProduct.Command command)
		{
			var product = await _mediator.Send(command);

			return StatusCode(201, product);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Update(int id, [FromBody]UpdateProduct.Command command)
		{
			command.Id = id;
			var product = await _mediator.Send(command);

			return Ok(product);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Remove(int id)
		{
			await _mediator.Send(new DeleteProduct.Command(id));

			return NoContent();
		}
	}
}
