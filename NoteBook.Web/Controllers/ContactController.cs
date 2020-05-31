using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        ContactClient apiClient;
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
            apiClient = new ContactClient(new Uri(configuration["BaseApiPath"]));

        }

        public async Task<IActionResult> Index()
        {

            var contacts = await apiClient.GetContacts(HttpContext.Session.GetString("Access_Token"));
            return View(contacts);

        }
        public async Task<ActionResult> Create(int Id)
        {
            await InitializeViewBag();
            if (Id > 0)
            {
                var note = await apiClient.GetContact(HttpContext.Session.GetString("Access_Token"), Id);

                if (note != null)
                    return View(note);
                return View();
            }
            return View();
        }

        private async Task<List<SelectListItem>> GetTitles()
        {
            var titles = await this.apiClient.GetTitles(HttpContext.Session.GetString("Access_Token"));
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in titles)
            {
                listItems.Add(new SelectListItem { Text = item.Item1, Value = item.Item2 });

            }

            return listItems;
        }
        private async Task<List<SelectListItem>> GetContactTypes()
        {
            var titles = await this.apiClient.GetContactTypes(HttpContext.Session.GetString("Access_Token"));
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in titles)
            {
                listItems.Add(new SelectListItem { Text = item.Item1, Value = item.Item2 });

            }

            return listItems;
        }
        private List<SelectListItem> GetContactWith()
        {
            // var titles = await this.apiClient.GetContacts(HttpContext.Session.GetString("Access_Token"));
            List<SelectListItem> listItems = new List<SelectListItem>();

            //foreach (var item in titles)
            //{
            //    //listItems.Add(new SelectListItem { Text = item.Item1, Value = item.Item2 });

            //}
            listItems.Add(new SelectListItem { Text = "Select", Value = "" });
            listItems.Add(new SelectListItem { Text = "Self", Value = "-1" });
            return listItems;
        }
        [HttpPost]
        public async Task<ActionResult> Create(ContactViewModel model)
        {
            await InitializeViewBag();
            if (!ModelState.IsValid)
                return View(model);

            var note = await apiClient.SaveContact(model, HttpContext.Session.GetString("Access_Token"));
            if (!note.IsSuccess)
                ModelState.AddModelError("", "Please try again.");

            return RedirectToAction("Index", "Contact");
        }

        private async Task InitializeViewBag()
        {
            ViewBag.ListOfTitle = await GetTitles();
            ViewBag.ContactTypes = await GetContactTypes();
            ViewBag.Contacts = GetContactWith();
        }
    }
}
