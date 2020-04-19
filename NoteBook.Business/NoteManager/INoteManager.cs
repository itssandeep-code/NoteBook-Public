using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Business.NoteManager
{
    public interface INoteManager
    {
        Task<Models.Note> GetNote(int Id);
        Task<IEnumerable<Models.Note>> GetNotes();
        Task<Models.Note> AddNote(Models.Note note);
        Task <Models.Note> UpdateNote(Models.Note note);
        Task<Models.Note> DeleteNote(int Id);
    }
}
