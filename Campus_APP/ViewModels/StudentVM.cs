using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Campus_APP.ViewModels
{
    public class StudentVM:BaseVM
    {
        private readonly StudentActions _studAct;
        private readonly StudentTypeActions _typeAct;
        private readonly RoomActions _roomAct;
        private readonly UniversityActions _uniAct;
        private readonly CampusActions _campusAct;
        
        public StudentVM()
        {
            _studAct = new StudentActions(this);
            _typeAct = new StudentTypeActions();
            _roomAct = new RoomActions(null);
            _uniAct = new UniversityActions(null);
            _campusAct = new CampusActions(null);
        }

        #region DataMembers
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _ssn;
        private bool _isExmatriculated;
        private int _idCampus;
        private int? _idRoom;
        private int _idType;
        private int _idUni;

        private ObservableCollection<StudentVM> _allStudents;
        private CampusRoomVM _campusRoom;
        private ObservableCollection<CampusRoomVM> _allRooms;
        private UniversityVM _university;
        private ObservableCollection<UniversityVM> _allUniversities;
        private CampusVM _campus;
        private ObservableCollection<CampusVM> _allCampuses;
        private StudentTypeVM _studentType;
        private ObservableCollection<StudentTypeVM> _allStudentTypes;


        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public string SSN
        {
            get { return _ssn; }
            set
            {
                _ssn = value;
                NotifyPropertyChanged("SSN");
            }
        }
        public bool IsExmatriculated
        {
            get { return _isExmatriculated; }
            set
            {
                _isExmatriculated = value;
                NotifyPropertyChanged("IsExmatriculated");
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
        public int? IdRoom
        {
            get { return _idRoom; }
            set
            {
                _idRoom = value;
                NotifyPropertyChanged("IdRoom");
            }
        }
        public int IdType
        {
            get { return _idType; }
            set
            {
                _idType = value;
                NotifyPropertyChanged("IdType");
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

        public ObservableCollection<StudentVM> AllStudents
        {
            get {
                _allStudents = _studAct.AllStudents();
                return _allStudents; }
            set
            {
                _allStudents = value;
                NotifyPropertyChanged("AllStudents");
            }
        }
        public CampusRoomVM CampusRoom
        {
            get {
                _campusRoom = _roomAct.GetRoomById(IdRoom);
                return _campusRoom; }
            set { _campusRoom = value;
                NotifyPropertyChanged("CampusRoom");
            }
        }
        public ObservableCollection<CampusRoomVM> AllRooms
        {
            get {
                _allRooms = _roomAct.GetRoomsAvailableForCampus(IdCampus);
                return _allRooms; }
            set
            {
                _allRooms = value;
                NotifyPropertyChanged("AllRooms");
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
            set
            {
                _allUniversities = value;
                NotifyPropertyChanged("AllUniversities");
            }
        }
        public CampusVM Campus
        {
            get {
                _campus = _campusAct.GetCampusById(IdCampus);
                return _campus; }
            set
            {
                _campus = value;
                NotifyPropertyChanged("Campus");
            }
        }
        public ObservableCollection<CampusVM> AllCampuses
        {
            get
            {
                _allCampuses = _campusAct.GetCampusesByUni(IdUni);
                return _allCampuses;
            }
            set
            {
                _allCampuses = value;
                NotifyPropertyChanged("AllCampuses");
            }
        }
        public StudentTypeVM StudentType
        {
            get {
                _studentType = _typeAct.GetType(IdType);
                return _studentType; }
            set
            {
                _studentType = value;
                NotifyPropertyChanged("StudentType");
            }
        }
        public ObservableCollection<StudentTypeVM> AllStudentTypes
        {
            get {
                _allStudentTypes = _typeAct.AllTypes();
                return _allStudentTypes; }
            set
            {
                _allStudentTypes = value;
                NotifyPropertyChanged("AllStudentTypes");
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
                    addCommand = new RelayCommand(_studAct.AddMethod);
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
                    deleteCommand = new RelayCommand(_studAct.DeleteMethod);
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
                    updateCommand = new RelayCommand(_studAct.UpdateMethod);
                }
                return updateCommand;
            }
        }
        #endregion

        public void UniChanged(int id)
        {
            AllCampuses = _campusAct.GetCampusesByUni(id);
        }
        public void CampusChanged(int id)
        {
            AllRooms = _roomAct.GetRoomsAvailableForCampus(id);
        }
    }
}
