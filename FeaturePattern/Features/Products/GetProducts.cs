using AutoMapper;
using API.Database.Repository.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Features.Products
{
    public static class GetProducts
	{
		public class Query : IRequest<List<Product>>
		{

		}

		public class Handler : IRequestHandler<Query, List<Product>>
		{
			private readonly IProductRepository _productRepository;

			public Handler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				return products;
			}
		}

	}
}
