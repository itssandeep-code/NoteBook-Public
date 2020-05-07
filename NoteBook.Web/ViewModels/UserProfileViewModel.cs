using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NoteBook.Web.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter mobile number.")]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Please enter a valid mobile number")]
        public string PhoneNumber { get; set; }
       
        public string ProfilePic { get; set; }
        // [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
