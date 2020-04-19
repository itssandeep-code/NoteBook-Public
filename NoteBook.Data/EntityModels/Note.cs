using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBook.Data.EntityModels
{
    [Table("Note", Schema = "dbo")]
    public class Note : BaseEntity
    {
      
        public string Subject { get; set; }
      
        public string Description { get; set; }
      
        public DateTime RemindMeOn { get; set; }
       
        public bool IsActive { get; set; }

    }
}
