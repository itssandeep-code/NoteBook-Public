using Microsoft.EntityFrameworkCore;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Implementation
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly NoteBookDbContext context;

        public ContactRepository(NoteBookDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ContactType>> GetContactType()
        {
            DbSet<ContactType> entities = this.context.Set<ContactType>();
            return await entities.ToListAsync();
        }

        public async Task Update(Contact entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Contact contact = this.context.Contacts.Find(entity.Id);
            //contact.IsActive = true;
            contact.LastName = entity.LastName;
            contact.ModifiedOn = DateTime.Now;    
            
            this.context.Contacts.Update(contact);
            await context.SaveChangesAsync();
        }
    }
}
