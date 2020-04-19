using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteBook.Web.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Subject.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter Description.")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime RemindMeOn { get; set; }
    }
}
