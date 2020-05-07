using Microsoft.EntityFrameworkCore;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public virtual async Task<T> Get(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public virtual async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            context.SaveChanges();
        }
     

        public virtual async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }


        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = entities;
            return await query.Where(filter).ToListAsync();
        }
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
