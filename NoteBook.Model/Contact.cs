using System;
using System.Collections.Generic;
using System.Text;

namespace NoteBook.Models
{
   public class Contact
    {
        public Int64 Id { get; set; }
        public int Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AlternateAddress { get; set; }
        public string Mobile { get; set; }
        public string AlternateMobile { get; set; }
        public string UserId { get; set; }
        public string PhotoPath { get; set; }
        public Int64? ContactWith { get; set; }
        public Int64 ContactTypeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfMarriage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
