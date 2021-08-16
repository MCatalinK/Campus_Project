using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Campus_APP.ViewModels
{
    public class UniversityVM:BaseVM
    {
        private readonly UniversityActions _uniAct;

        public UniversityVM()
        {
            _uniAct = new UniversityActions(this);
        }

        #region DataMembers
        private int _id;
        private string _name;
        private ObservableCollection<UniversityVM> _allUniversities;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public ObservableCollection<UniversityVM> AllUniversities
        {
            get {
                _allUniversities = _uniAct.AllUniversities();
                return _allUniversities; }
            set { _allUniversities = value;
                NotifyPropertyChanged("AllUniversities");
            }
        }
        #endregion

        #region Commands
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(_uniAct.AddMethod);
                }
                return addCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(_uniAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(_uniAct.UpdateMethod);
                }
                return updateCommand;
            }
        }
        #endregion
    }
}
