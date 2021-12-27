using Noter.Common.DataProvider;
using Noter.ViewModel.Command;
using System.Collections.ObjectModel;

namespace Noter.ViewModel;

public class MainViewModel : ViewModelBase
{
    private NoteViewModel _selectedNote;
    private readonly INoteDataProvider _noteDataProvider;

    public MainViewModel(INoteDataProvider noteDataProvider)
    {
        _noteDataProvider = noteDataProvider;
        LoadCommand = new Commander(Load);
        NewNoteCommand = new Commander(New);
    }
    public Commander LoadCommand { get; }
    public Commander NewNoteCommand { get; }
    public ObservableCollection<NoteViewModel> Notes { get; } = new();
    public NoteViewModel SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (_selectedNote != value)
            {
                _selectedNote = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsNoteSelected));
            }
        }
    }
    public bool IsNoteSelected => SelectedNote != null;

    public void Load()
    {
        var notes = _noteDataProvider.LoadNotes();

        Notes.Clear();

        foreach (var note in notes)
        {
            Notes.Add(new NoteViewModel(note, _noteDataProvider));
        }
    }
    public void New()
    {
        _noteDataProvider.NewNote();
    }
}
