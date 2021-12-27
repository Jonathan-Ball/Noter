using Noter.Common.Model;

namespace Noter.Common.DataProvider
{
    public interface INoteDataProvider
    {
        void NewNote();
        IEnumerable<Note> LoadNotes();
        void SaveNote(Note note);
    }
}
