using AutoMapper;
using API.Database.Repository.Contracts;
using API.Features.Products.Command;
using API.Features.Products.Command.Validator;
using API.Core.Helpers.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Features.Products
{
    public static class UpdateProduct
	{
		public class Command : ProductCommand, IRequest<Product>
		{
            [JsonIgnore]
			public int Id { get; set; }
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
				var product = await _productRepository.Get(a => a.Id == request.Id);

				if (product == null)
					throw new NotFoundException();

				_mapper.Map(request, product);

				_productRepository.Update(product);

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
