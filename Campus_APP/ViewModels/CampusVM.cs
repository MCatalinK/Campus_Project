using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using Campus_APP.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Campus_APP.ViewModels
{
    public class CampusVM : BaseVM
    {
        private readonly UniversityActions _uniAct;
        private readonly CampusActions _campAct;

        public CampusVM()
        {
            _uniAct = new UniversityActions(null);
            _campAct = new CampusActions(this);
        }

        #region DataMembers
        private int _id;
        private decimal _tax;
        private int _idUni;
        private UniversityVM _university;
        private ObservableCollection<UniversityVM> _allUniversities;
        private ObservableCollection<CampusVM> _allCampuses;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public decimal Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                NotifyPropertyChanged("Tax");
            }
        }
        public int IdUni
        {
            get { return _idUni; }
            set
            {
                _idUni = value;
                NotifyPropertyChanged("IdUni");
            }
        }
        public UniversityVM University
        {
            get {
                _university = _uniAct.GetUniversityById(IdUni);
                return _university; }
            set
            {
                _university = value;
                NotifyPropertyChanged("University");
            }
        }
        public ObservableCollection<UniversityVM> AllUniversities
        {
            get {
                _allUniversities = _uniAct.AllUniversities();
                return _allUniversities; }
        }
        public ObservableCollection<CampusVM> AllCampuses
        {
            get {
                //_allCampuses = _campAct.GetCampusesByUni(IdUni);
                return _allCampuses; }
            set
            {
                _allCampuses = value;
                NotifyPropertyChanged("AllCampuses");
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
                    addCommand = new RelayCommand(_campAct.AddMethod);
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
                    deleteCommand = new RelayCommand(_campAct.DeleteMethod);
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
                    updateCommand = new RelayCommand(_campAct.UpdateMethod);
                }
                return updateCommand;
            }
        }
        private ICommand getRoomsCommand;
        public ICommand GetRoomsCommand
        {
            get
            {
                if (getRoomsCommand == null)
                {
                    getRoomsCommand = new RelayCommand(GetRooms);
                }
                return getRoomsCommand;
            }
        }
        #endregion

        public void Load(int id)
        {
            AllCampuses = _campAct.GetCampusesByUni(id);
        }
        public void GetRooms(object obj)
        {
            var campus = obj as CampusVM;
            if (campus is null) return;
            int id = campus.Id;
            RoomsView roomsView = new RoomsView(new CampusRoomVM() { IdCampus = id });
            roomsView.ShowDialog();
        }

    }
}
