using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBook.Business.NoteManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteBook.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }
        [HttpGet]
        [Route("GetNotes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Models.Note>>> GetNotes()
        {
            IEnumerable<Models.Note> response = new List<Models.Note>();
            var UserId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;            
            var result = await noteManager.GetNotes(UserId);
            if (!result.Any())
                return NotFound(response);

            return Ok(result);
        }
        [HttpGet]
        [Route("GetNote")]
        public async Task<ActionResult<Models.Note>> GetNote(int Id)
        {
            var result = await noteManager.GetNote(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        [Route("SaveNote")]
        public async Task<ActionResult<Models.Note>> SaveNote([FromBody] Models.Note note)
        {
            var UserId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (note.Id > 0)
            {                
                await noteManager.UpdateNote(note);
            }
            else
            {
                note.IsActive = true;
                note.CreatedBy = UserId;
                note.CreatedOn = DateTime.Now;
                await noteManager.AddNote(note);
            }
            return Ok(new { ReturnMessage = "Note saved successfully", IsSuccess = true, Data = note });
            
        }
        [HttpPost]
        [Route("DeleteNote")]
        public async Task<ActionResult<Models.Note>> DeleteNote([FromBody] Models.Note note)
        {
            var Note = await noteManager.DeleteNote(Convert.ToInt32(note.Id));
            return Ok(Note);
        }

    }
}
