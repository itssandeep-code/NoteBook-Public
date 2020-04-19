using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoteBook.Data.EntityModels
{
   public class ContactType:BaseEntity
    {
        [Column(Order = 1)]
        [Required]
        public string TypeName { get; set; }
       
    }
}
