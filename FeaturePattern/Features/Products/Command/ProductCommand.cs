using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Features.Products.Command
{
	public class ProductCommand
	{
		public string Name { get; set; }

		public decimal Price { get; set; }

		public string Description { get; set; }

		public int Quantity { get; set; }
	}
}
