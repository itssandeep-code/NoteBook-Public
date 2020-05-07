using AutoMapper;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Business.NoteManager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;
        private readonly IMapper mapper;

        public NoteManager(INoteRepository noteRepository, IMapper mapper)
        {
            this.noteRepository = noteRepository;
            this.mapper = mapper;
        }

        public async Task<Models.Note> AddNote(Models.Note note)
        {
            await this.noteRepository.Insert(mapper.Map<Note>(note));
            return note;

        }

        public async Task<Models.Note> DeleteNote(int Id)
        {
            Note note = await this.noteRepository.Get(Id);
            await noteRepository.Delete(note);
            //  noteRepository.SaveChanges();
            return mapper.Map<Models.Note>(note);
        }

        public async Task<Models.Note> GetNote(int Id)
        {
            Note note = await this.noteRepository.Get(Id);
            return mapper.Map<Models.Note>(note);
        }
        public async Task<IEnumerable<Models.Note>> GetNotes()
        {
            var notes = await this.noteRepository.GetAll();
            return mapper.Map<IEnumerable<Models.Note>>(notes);
            //return null;
        }
        public async Task<IEnumerable<Models.Note>> GetNotes(string UserId)
        {
            var notes = await this.noteRepository.GetAll(x=>x.CreatedBy==UserId);
            return mapper.Map<IEnumerable<Models.Note>>(notes);
            //return null;
        }

        public async Task<Models.Note> UpdateNote(Models.Note note)
        {
            await this.noteRepository.Update(mapper.Map<Note>(note));
            return note;
        }
    }
}
