using Microsoft.AspNetCore.Http;
using System;

namespace NoteBook.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }        
        public string ProfilePic { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
