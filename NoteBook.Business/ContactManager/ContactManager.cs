using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;


namespace NoteBook.Business.ContactManager
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;

        public ContactManager(IContactRepository contactRepository, IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
        }
        public async Task<Models.Contact> AddContact(Models.Contact contact)
        {
            await this.contactRepository.Insert(mapper.Map<Contact>(contact));
            return contact;
        }

        public async Task<Models.Contact> DeleteContact(int Id)
        {
            Contact contact= await this.contactRepository.Get(Id);
            await contactRepository.Delete(contact);
            //  noteRepository.SaveChanges();
            return mapper.Map<Models.Contact>(contact);
        }

        public async Task<Models.Contact> GetContact(int Id)
        {
            Contact contact = await this.contactRepository.Get(Id);
            return mapper.Map<Models.Contact>(contact);
        }

        public async Task<IEnumerable<Models.Contact>> GetContacts(string UserId)
        {
            var contacts = await this.contactRepository.GetAll(x => x.UserId == UserId);
            return mapper.Map<IEnumerable<Models.Contact>>(contacts);
            //return null;
        }

        public async Task<Models.Contact> UpdateContact(Models.Contact contact)
        {
            await this.contactRepository.Update(mapper.Map<Contact>(contact));
            return contact;
        }
        public async Task<IEnumerable<Models.ContactType>> GetContactTypes()
        {
            var contacts = await this.contactRepository.GetContactType();
            return mapper.Map<IEnumerable<Models.ContactType>>(contacts);
        }
    }
}
