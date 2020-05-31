using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Business.ContactManager
{
   public interface IContactManager
    {
        Task<Models.Contact> GetContact(int Id);
        Task<IEnumerable<Models.Contact>> GetContacts(string UserId);
        Task<Models.Contact> AddContact(Models.Contact contact);
        Task<Models.Contact> UpdateContact(Models.Contact contact);
        Task<Models.Contact> DeleteContact(int Id);
        Task<IEnumerable<Models.ContactType>> GetContactTypes(); 
    }
}
