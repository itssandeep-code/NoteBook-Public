using Microsoft.AspNetCore.Http;
using NoteBook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NoteBook.Web.ViewModels
{
    public class ContactViewModel
    {

        public int Id { get; set; }

        [Required]
        public int? Title { get; set; }
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter address.")]
        public string Address { get; set; }
        public string AlternateAddress { get; set; }
        [Required(ErrorMessage = "Please enter mobile number.")]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Please enter a valid mobile number")]
        public string Mobile { get; set; }
        [RegularExpression(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Please enter a valid mobile number")]
        public string AlternateMobile { get; set; }

       // [Required(ErrorMessage ="Please enter DOB")]        
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
                
        [Display(Name = "Date Of Marriage")]
        [DataType(DataType.Date)]
        public DateTime? DateOfMarriage { get; set; }

        public string UserId { get; set; }
        public string PhotoPath { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        [Required]
        public int? ContactTypeId { get; set; }
        [Required]
        public int? ContactWith { get; set; }
    }
}
