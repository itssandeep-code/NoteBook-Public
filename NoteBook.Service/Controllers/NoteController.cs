using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBook.Business.NoteManager;
using System.Collections.Generic;
using System.Linq;
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
            var result = await noteManager.GetNotes();
            if (!result.Any())
                return NotFound(new { ReturnMessage = "No record exists.", IsSuccess = false });

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

            if (note.Id > 0)
                await noteManager.UpdateNote(note);
            else
                await noteManager.AddNote(note);

            return Ok(note);
        }
        [HttpPost]
        [Route("RemoveNote")]
        public async Task<ActionResult<Models.Note>> RemoveNote(int Id)
        {
            var Note = await noteManager.DeleteNote(Id);
            return Ok(Note);
        }

    }
}
