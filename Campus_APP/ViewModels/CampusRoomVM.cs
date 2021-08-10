using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Campus_APP.ViewModels
{
    public class CampusRoomVM:BaseVM
    {
        private readonly RoomActions _roomAct;

        public CampusRoomVM()
        {
            _roomAct = new RoomActions(this);
        }

        #region DataMemebers
        private int _id;
        private int _noRoom;
        private bool _isOccupied;
        private int _idCampus;
        private ObservableCollection<CampusRoomVM> _allRooms;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public int NoRoom
        {
            get { return _noRoom; }
            set
            {
                _noRoom = value;
                NotifyPropertyChanged("NoRoom");
            }
        }
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set
            {
                _isOccupied = value;
                NotifyPropertyChanged("IsOccupied");
            }
        }
        public int IdCampus
        {
            get { return _idCampus; }
            set
            {
                _idCampus = value;
                NotifyPropertyChanged("IdCampus");
            }
        }
        public ObservableCollection<CampusRoomVM> AllRooms
        {
            get {
                _allRooms = _roomAct.GetRoomsByCampus(IdCampus);
                return _allRooms; }
            set
            {
                _allRooms = value;
                NotifyPropertyChanged("AllRooms");
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
                    addCommand = new RelayCommand(_roomAct.AddMethod);
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
                    deleteCommand = new RelayCommand(_roomAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }
        #endregion
    }
}
