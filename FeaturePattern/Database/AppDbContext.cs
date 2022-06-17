using API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Database
{
    public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}
		public DbSet<Product> Products { get; set; }

		#region Transaction
		private IDbContextTransaction _currentTransaction;
		private bool HasTransaction { get => _currentTransaction != null; }

		public async Task BeginTransaction(CancellationToken cancellationToken)
		{
			if (HasTransaction) return;

			_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
		}

		public async Task CommitTransaction(CancellationToken cancellationToken)
		{
			try
			{
				await SaveChangesAsync(cancellationToken);
				_currentTransaction.Commit();
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw;
			}
			finally
			{
				if (HasTransaction)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}

			}
		}

		public void RollbackTransaction()
		{
			try
			{
				_currentTransaction.Rollback();
			}
			finally
			{
				if (HasTransaction)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}

			}
		}

		#endregion
	}
}
