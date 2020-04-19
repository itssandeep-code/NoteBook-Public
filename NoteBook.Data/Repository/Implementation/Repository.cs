using Microsoft.EntityFrameworkCore;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly NoteBookDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(NoteBookDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> Get(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            context.SaveChanges();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var note = this.context.Update(entity);
            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
