using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteBook.Web.ServiceClient
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; }
    }
}
