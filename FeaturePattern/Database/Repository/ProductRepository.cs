using API.Core.Entities;
using API.Database.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Repository
{
    public class ProductRepository : DefaultRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
