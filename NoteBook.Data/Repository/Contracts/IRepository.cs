using NoteBook.Data.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(long id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
  
        void SaveChanges();
    }
}
