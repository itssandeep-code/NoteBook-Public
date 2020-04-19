
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteBook.Data.EntityModels;

namespace NoteBook.Data
{  
    public class NoteBookDbContext : IdentityDbContext<IdentityUser>
    {
        public NoteBookDbContext(DbContextOptions<NoteBookDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
