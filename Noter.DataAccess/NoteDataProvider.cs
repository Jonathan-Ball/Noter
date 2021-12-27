using Noter.Common.DataProvider;
using Noter.Common.Model;
using System.Diagnostics;
using System.Text;

namespace Noter.DataAccess
{
    public class NoteDataProvider : INoteDataProvider
    {
        private string _dataDirectory = @$"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName}\Noter.DataAccess\Data";
        public void NewNote()
        {
            string[] files = Directory.GetFiles(_dataDirectory, "*.txt");

            for (int i = 1; i < short.MaxValue; i++)
            {

                if (!files.Contains($@"{_dataDirectory}\NewFile{i}.txt"))
                {
                    File.Create($@"{_dataDirectory}\NewFile{i}.txt").Close();
                    break;
                }
            }
        }

        public IEnumerable<Note> LoadNotes()
        {
            Debug.WriteLine("Loading notes...");
            List<Note> notes = new List<Note>();
            try
            {
                string[] files = Directory.GetFiles(_dataDirectory, "*.txt");
                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        Debug.WriteLine($"Loading: {fileName}");
                        notes.Add(new Note(File.ReadAllText(file), fileName));
                    }
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Loading notes failed: {ex}");
            }
            return notes;
        }

        public void SaveNote(Note note)
        {
            StreamWriter streamWriter = null;
            string file = $@"{_dataDirectory}\{note.Title}";
            if (!File.Exists(file))
            {
                try
                {
                    File.Create(file).Close();
                    streamWriter = new(file, true, Encoding.ASCII);
                    foreach (char character in note.Text)
                    {
                        streamWriter.Write(character);
                    }
                    streamWriter.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"UNPOSSIBLE to perform action: {ex}");
                }
                finally
                {
                    streamWriter.Dispose();
                    Debug.WriteLine($"NOTE Saved: {note.Title}");
                }
            }
        }
    }
}
