using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoteBook.Data.EntityModels
{
   public class BaseEntity
    {
        [Column(Order = 0)]
        public Int64 Id { get; set; }
        [Column(Order = 100)]
        public string CreatedBy { get; set; }
        [Column(Order = 101)]
        public DateTime CreatedOn { get; set; }
        [Column(Order = 102)]
        public string ModifiedBy { get; set; }
        [Column(Order =103)]
        public DateTime? ModifiedOn { get; set; }
    }
}
