using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NoteBook.Web.ServiceClient;
using NoteBook.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace NoteBook.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        NoteClient apiClient;
        private readonly IConfiguration configuration;

        public NoteController(IConfiguration configuration)
        {
            this.configuration = configuration;
            apiClient = new NoteClient(new Uri(configuration["BaseApiPath"]));

        }

        public async Task<IActionResult> Index()
        {

            var notes = await apiClient.GetNotes(HttpContext.Session.GetString("Access_Token"));
            return View(notes);
        }
        public async Task<IActionResult> Create(int Id)
        {
            if (Id > 0)
            {
                var note = await apiClient.GetNote(HttpContext.Session.GetString("Access_Token"), Id);

                if (note != null)
                    return View(note);
                return View();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NoteViewModel model)
        {
            //db.Doctors.Add(doctor);
            //db.SaveChanges();
            if (!ModelState.IsValid)
                return View(model);

            var note = await apiClient.SaveNote(model, HttpContext.Session.GetString("Access_Token"));
            if (!note.IsSuccess)
                ModelState.AddModelError("", "Please try again.");

            return RedirectToAction("Index", "Note");
        }
        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            var note = await apiClient.DeleteNote(HttpContext.Session.GetString("Access_Token"), new NoteViewModel { Id = id });
            if (note != null)
                return true;
            return false;
        }
    }
}
