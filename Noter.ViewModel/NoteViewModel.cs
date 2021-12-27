using Noter.Common.DataProvider;
using Noter.Common.Model;
using Noter.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly Note _note;
        private readonly INoteDataProvider _noteDataProvider;

        public NoteViewModel(Note note, INoteDataProvider noteDataProvider)
        {
            _note = note;
            _noteDataProvider = noteDataProvider;
            SaveCommand = new Commander(Save, () => CanSave);
        }

        public Commander SaveCommand { get; private set; }

        public string NoteTitle
        {
            get => _note.Title;
            set
            {
                if (_note.Title != value)
                {
                    _note.Title = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CanSave));
                }
            }
        }
        public string NoteText
        {
            get { return _note.Text; }
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CanSave));
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public bool CanSave => !string.IsNullOrWhiteSpace(NoteText) && !string.IsNullOrWhiteSpace(NoteTitle);

        public void Save()
        {
            _noteDataProvider.SaveNote(_note);
        }
    }
}
