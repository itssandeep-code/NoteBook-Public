using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoteBook.Models
{
    public enum Title
    {
        Mr=1,
        [Display(Name="Mrs.")]
        Mrs,
        Miss
    }
}
