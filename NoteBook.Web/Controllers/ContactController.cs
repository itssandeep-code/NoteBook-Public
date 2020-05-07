using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NoteBook.Web.ServiceClient;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Web.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        NoteClient apiClient;
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
            apiClient = new NoteClient(new Uri(configuration["BaseApiPath"]));

        }

        public async Task<IActionResult> Index()
        {
            // var notes = await apiClient.GetNotes(HttpContext.Session.GetString("Access_Token"));
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            return View(contacts);
        }
        public ActionResult Create(int Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ContactViewModel model)
        {
            if (!ModelState.IsValid) { }
            return View(model);
        }
    }
}
