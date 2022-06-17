using API.Database;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Helpers.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _dbContext;
        private readonly IEnumerable<string> _methods = new List<string> { "POST", "PUT", "DELETE", "PATCH" };

        public DbContextTransactionFilter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cancellationToken = context.HttpContext.RequestAborted;

            if (!_methods.Contains(context.HttpContext.Request.Method))
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                await next();
                return;
            }

            try
            {
                await _dbContext.BeginTransaction(cancellationToken);

                var actionExecuted = await next();
                if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
                    _dbContext.RollbackTransaction();
                else
                    await _dbContext.CommitTransaction(cancellationToken);
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
