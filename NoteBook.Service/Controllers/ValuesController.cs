using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteBook.Business.NoteManager;
using System.Collections.Generic;

namespace NoteBook.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly IMapper mapper;
        //private readonly INoteManager noteManager;

        //public ValuesController(IMapper mapper, INoteManager noteManager)
        //{
        //    this.mapper = mapper;
        //    this.noteManager = noteManager;
        //}
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           // var note = new NoteBook.Data.EntityModels.Note
           // {
           //     Id = 1,
           //     Subject = "Sandeep",
           //     Description="a",
           //     IsActive=true
           // };
           //var note1= mapper.Map<NoteBook.Models.Note>(note);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
