using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Features.Products.Command.Validator
{
	public class ProductValidator<T> : AbstractValidator<T> where T : ProductCommand
	{

		protected void ValidateName()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("This field must not be null")
				.Length(2, 30).WithMessage("The Name must have between 2 and 30 characters");
		}

		protected void ValidatePrice()
		{
			RuleFor(c => c.Price)
				.GreaterThan(0).WithMessage("The price must be greater than 0");
		}

		protected void ValidateQuantity()
		{
			RuleFor(c => c.Quantity)
				.GreaterThanOrEqualTo(0).WithMessage("The quantity must be greater or equal to 0");
		}

		protected void ValidateDescription()
		{
			RuleFor(c => c.Description)
				.NotEmpty().WithMessage("This field must not be null")
				.Length(5, 150).WithMessage("The Description must have between 5 and 150 characters");
		}
	}
}
