using API.Database.Repository;
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
	public static class DeleteProduct
	{
		public class Command : IRequest<Unit>
		{
			public int Id { get; set; }

			public Command(int id)
			{
				Id = id;
			}
		}

		public class Handler : IRequestHandler<Command, Unit>
		{
			private readonly IProductRepository _productRepository;

			public Handler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var product = await _productRepository.Get(a => a.Id == request.Id);

				if (product == null)
					throw new NotFoundException();

				_productRepository.Remove(product);

				return Unit.Value;
			}
		}
	}
}
