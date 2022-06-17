using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Database.Repository.Contracts
{
    public interface IDefaultRepository<T>
    {
        Task Add(T obj);

        void Update(T obj);

        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAll(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAll();

        void Remove(T obj);
    }
}
