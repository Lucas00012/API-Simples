using API.Core.Entities;
using API.Database.Repository.Contracts;
using API.Core.Helpers.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Features.Products
{
    public static class GetProduct
	{
		public class Query : IRequest<Product>
		{
			public int Id { get; set; }

			public Query(int id)
			{
				Id = id;
			}

		}

		public class Handler : IRequestHandler<Query, Product>
		{
			private readonly IProductRepository _productRepository;

			public Handler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Product> Handle(Query request, CancellationToken cancellationToken)
			{
				var product = await _productRepository.Get(a => a.Id == request.Id);

				if (product == null)
					throw new NotFoundException();

				return product;
			}
		}
	}
}
