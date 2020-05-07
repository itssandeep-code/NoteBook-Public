using Microsoft.EntityFrameworkCore;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using System;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Implementation
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        private readonly NoteBookDbContext context;

        public NoteRepository(NoteBookDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task Update(Note entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Note note = this.context.Notes.Find(entity.Id);
            note.IsActive = true;
            note.ModifiedOn = DateTime.Today;
            note.Subject = entity.Subject;
            note.Description = entity.Description;
            note.RemindMeOn = entity.RemindMeOn;
            this.context.Notes.Update(note);
            await context.SaveChangesAsync();
        }
    }
}
