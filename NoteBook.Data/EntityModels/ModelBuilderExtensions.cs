using Microsoft.EntityFrameworkCore;
using System;

namespace NoteBook.Data.EntityModels
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>().HasData(
                   new ContactType
                   {
                       Id=1,
                       TypeName = "Father",
                       CreatedBy = "1",
                       CreatedOn = DateTime.Today
                   },
                    new ContactType
                    {
                        Id = 2,
                        TypeName = "Mother",
                        CreatedBy = "1",
                        CreatedOn = DateTime.Today
                    },
                     new ContactType
                     {
                         Id = 3,
                         TypeName = "Brother",
                         CreatedBy = "1",
                         CreatedOn = DateTime.Today
                     }, new ContactType
                     {
                         Id = 4,
                         TypeName = "Sister",
                         CreatedBy = "1",
                         CreatedOn = DateTime.Today
                     },
                      new ContactType
                      {
                          Id = 5,
                          TypeName = "Wife",
                          CreatedBy = "1",
                          CreatedOn = DateTime.Today
                      },
                       new ContactType
                       {
                           Id = 6,
                           TypeName = "Husband",
                           CreatedBy = "1",
                           CreatedOn = DateTime.Today
                       },
                        new ContactType
                        {
                            Id = 7,
                            TypeName = "Son",
                            CreatedBy = "1",
                            CreatedOn = DateTime.Today
                        },
                         new ContactType
                         {
                             Id = 8,
                             TypeName = "Daughter",
                             CreatedBy = "1",
                             CreatedOn = DateTime.Today
                         }
               );
        }
    }
}
