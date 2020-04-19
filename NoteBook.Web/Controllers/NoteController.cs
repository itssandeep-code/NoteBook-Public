using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NoteBook.Web.ServiceClient;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Web.Controllers
{
    [Authorize]
    public class NoteController:Controller
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

             var notes =await apiClient.GetNotes(HttpContext.Session.GetString("Access_Token"));
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(configuration["BaseApiPath"]);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    // Add the Authorization header with the AccessToken.
            //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("Access_Token"));

            //    // create the URL string.
            //    string url = string.Format("" + configuration["BaseApiPath"] + "/Note/GetNotes");

            //    var json = JsonConvert.SerializeObject("");
            //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); //
            //    // make the request
            //    HttpResponseMessage response = await client.GetAsync(url);

            //    // parse the response and return the data.
            //    string jsonString = await response.Content.ReadAsStringAsync();
            //    object responseData = JsonConvert.DeserializeObject(jsonString);
            //    var userName = (dynamic)responseData;

            //}

            return View(notes);
        }
        //public ActionResult Create(int Id)
        //{
        //    if (Id > 0)
        //    {
        //        var result = this.noteRepository.GetNote(Id);
        //        if (result != null)
        //            return View(new NoteViewModel { Id = result.Id, Subject = result.Subject, Description = result.Description, RemindMe = result.RemindMe });
        //        return View();
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(NoteViewModel model)
        //{
        //    //db.Doctors.Add(doctor);
        //    //db.SaveChanges();
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var note = new Models.Note
        //    {
        //        Subject = model.Subject,
        //        Description = model.Description,
        //        IsActive = true,
        //        RemindMe = model.RemindMe,
        //        CreatedBy = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
        //        CreatedOn = DateTime.Today

        //    };
        //    if (model.Id > 0)
        //    {
        //        note.Id = model.Id;
        //        this.noteRepository.Update(note);
        //    }
        //    else
        //        this.noteRepository.Add(note);

        //    return RedirectToAction("Index", "Note");
        //}
        //[HttpPost]
        //public bool Delete(int id)
        //{
        //    var note = this.noteRepository.Delete(id);
        //    if (note != null)
        //        return true;
        //    return false;
        //}
    }
}
