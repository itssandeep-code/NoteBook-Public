using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteBook.Data.EntityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }     
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }

    }
}
