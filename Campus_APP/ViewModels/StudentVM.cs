using Campus_APP.Helpers;
using System.Collections.ObjectModel;

namespace Campus_APP.ViewModels
{
    class StudentVM:BaseVM
    {
        #region DataMembers
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _ssn;
        private int _idCampus;
        private int _idRoom;
        private int _idType;
        private int _idUni;
        private int _idUser;

        private CampusRoomVM _campusRoom;
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
        public int IdCampus
        {
            get { return _idCampus; }
            set
            {
                _idCampus = value;
                NotifyPropertyChanged("IdCampus");
            }
        }
        public int IdRoom
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
        public int IdUser
        {
            get { return _idUser; }
            set
            {
                _idUser = value;
                NotifyPropertyChanged("IdUser");
            }
        }   
        public CampusRoomVM CampusRoom
        {
            get { return _campusRoom; }
            set { _campusRoom = value;
                NotifyPropertyChanged("CampusRoom");
            }
        }
        public UniversityVM University
        {
            get { return _university; }
            set
            {
                _university = value;
                NotifyPropertyChanged("University");
            }
        }
        public ObservableCollection<UniversityVM> AllUniversities
        {
            get { return _allUniversities; }
            set
            {
                _allUniversities = value;
                NotifyPropertyChanged("AllUniversities");
            }
        }
        public CampusVM Campus
        {
            get { return _campus; }
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
                return _allCampuses;
            }
        }
        public StudentTypeVM StudentType
        {
            get { return _studentType; }
            set
            {
                _studentType = value;
                NotifyPropertyChanged("StudentType");
            }
        }
        public ObservableCollection<StudentTypeVM> AllStudentTypes
        {
            get { return _allStudentTypes; }
        }
        #endregion
    }
}
