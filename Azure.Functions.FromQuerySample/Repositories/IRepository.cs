using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.Functions.FromQuerySample
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> Get(Guid id);
        Task<ICollection<T>> GetMany();
        Task<ICollection<T>> GetMany(params Guid[] ids);
        Task<ICollection<T>> GetMany(params int[] ids);
        Task Put(T product);
        Task Delete(Guid id);
    }
}
