using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBook.Business.ContactManager;
using NoteBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteBook.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactManager contactManager;

        public ContactController(IContactManager contactManager)
        {
            this.contactManager = contactManager;
        }
        [HttpGet]
        [Route("GetTitles")]
        [Authorize]
        public List<Tuple<string,string>> GetTitles()
        {
            var titleName= Enum.GetNames(typeof(Title));
            List<Tuple<string, string>> response = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Select","")
            };
            for (int item = 0; item < titleName.Length; item++)
            {
                response.Add(new Tuple<string, string>(titleName[item], Convert.ToString(item + 1)));
            }
            return response;
        }
        [HttpGet]
        [Route("GetContactTypes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tuple<string, string>>>> GetContactTypes()        
        {
            List<Tuple<string, string>> response = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Select","")
            };
            var UserId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await contactManager.GetContactTypes();
            if (!result.Any())
                return NotFound(response);

            foreach (var item in result)
            {
                response.Add(new Tuple<string, string>(item.TypeName,Convert.ToString(item.Id)));
            }

            return Ok(response);
            
        }
        [HttpGet]
        [Route("GetContacts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Models.Contact>>> GetContacts()
        {
            IEnumerable<Models.Contact> response = new List<Models.Contact>();
            var UserId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await contactManager.GetContacts(UserId);
            if (!result.Any())
                return NotFound(response);

            return Ok(result);
        }
        [HttpGet]
        [Route("GetContact")]
        [Authorize]
        public async Task<ActionResult<Models.Contact>> GetContact(int Id)
        {
            var result = await contactManager.GetContact(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        [Route("SaveContact")]
        [Authorize]
        public async Task<ActionResult<Models.Contact>> SaveContact([FromBody] Models.Contact contact)
        {
            var UserId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (contact.Id > 0)
            {
                await contactManager.UpdateContact(contact);
            }
            else
            {
                contact.IsActive = true;
                contact.CreatedBy = UserId;
                contact.CreatedOn = DateTime.Now;
                contact.UserId = UserId;
                await contactManager.AddContact(contact);
            }
            return Ok(new { ReturnMessage = "Contact saved successfully", IsSuccess = true, Data = contact });
        }
    }
}
