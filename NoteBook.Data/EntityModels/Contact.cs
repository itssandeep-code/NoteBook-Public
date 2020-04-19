using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoteBook.Data.EntityModels
{
    public class Contact:BaseEntity
    {
        [Column(Order = 1)]
        [Required]
        public string FirstName { get; set; }
        [Column(Order = 2)]
        [Required]
        public string LastName { get; set; }
        [Column(Order = 3)]
        public string Email { get; set; }
        [Column(Order = 4)]
        public string Address { get; set; }
        [Column(Order = 5)]
        public string AlternateAddress { get; set; }
        [Column(Order = 6)]
        [Required]
        public string Mobile { get; set; }
        [Column(Order = 7)]
        public string AlternateMobile { get; set; }
        [Column(Order = 8)]
        public string UserId { get; set; }
        [Column(Order = 9)]
        public string PhotoPath { get; set; }
        [Column(Order = 10)]
        public Int64? ContactWith { get; set; }
        [ForeignKey("ContactTypeId")]
        public ContactType ContactType { get; set; }     
        [Column(Order = 11)]
        public Int64 ContactTypeId { get; set; }
        [Column(Order = 12)]
        public DateTime DateOfBirth { get; set; }
        [Column(Order = 13)]
        public DateTime DateOfMarriage { get; set; }
    }
}
