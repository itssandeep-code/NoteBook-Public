using NoteBook.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> Get(long id);
        Task Insert(T entity);     
        Task Delete(T entity);
  
        void SaveChanges();
    }
}
