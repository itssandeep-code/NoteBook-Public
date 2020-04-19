using System;

namespace NoteBook.Models
{
    public class Note
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime RemindMeOn { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
