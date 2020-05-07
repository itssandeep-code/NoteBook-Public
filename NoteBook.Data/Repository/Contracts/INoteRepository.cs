using NoteBook.Data.EntityModels;
using System.Threading.Tasks;

namespace NoteBook.Data.Repository.Contracts
{
    public interface INoteRepository : IRepository<Note>
    {
        Task Update(Note note);
    }
}
