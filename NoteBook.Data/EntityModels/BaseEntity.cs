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
        [Column(Order = 1)]
        public string CreatedBy { get; set; }
        [Column(Order = 2)]
        public DateTime CreatedOn { get; set; }
        [Column(Order = 3)]
        public string ModifiedBy { get; set; }
        [Column(Order =4)]
        public DateTime? ModifiedOn { get; set; }
    }
}
