using AutoMapper;
using API.Database.Repository.Contracts;
using API.Features.Products.Command;
using API.Features.Products.Command.Validator;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Features.Products
{
    public static class AddProduct
	{
		public class Command : ProductCommand, IRequest<Product>
		{

		}

		public class CommandValidator : ProductValidator<Command>
		{
			public CommandValidator()
			{
				ValidateName();
				ValidatePrice();
				ValidateDescription();
				ValidateQuantity();
			}
		}

		public class Handler : IRequestHandler<Command, Product>
		{
			private readonly IProductRepository _productRepository;
			private readonly IMapper _mapper;

			public Handler(IProductRepository productRepository, IMapper mapper)
			{
				_productRepository = productRepository;
				_mapper = mapper;
			}

			public async Task<Product> Handle(Command request, CancellationToken cancellationToken)
			{
				var product = _mapper.Map<Product>(request);

				await _productRepository.Add(product);

				return product;
			}
		}

		public class MappingProfile : Profile
		{
			public MappingProfile()
			{
				CreateMap<Command, Product>();
			}
		}
	}
}
