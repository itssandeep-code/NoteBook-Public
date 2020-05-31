using NoteBook.Data.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Contracts
{
    
    public interface IContactRepository : IRepository<Contact>
    {
        Task Update(Contact contact);
        Task<IEnumerable<ContactType>> GetContactType();
    }
}
