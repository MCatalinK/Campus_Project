using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Campus_APP.ViewModels
{
    public class InvoiceVM:BaseVM
    {
        private readonly StudentActions _studAct;
        private readonly InvoiceActions _invoiceAct;

        public InvoiceVM()
        {
            _studAct = new StudentActions(null);
            _invoiceAct = new InvoiceActions(this);
        }

        #region DataMemebers
        private int _id;
        private string _month;
        private DateTime _deadendDate;
        private DateTime? _datePayed;
        private decimal _total;
        private bool _isPayed;
        private int _idStudent;

        private StudentVM _student;
        private StudentTypeVM _studentType;
        private UniversityVM _university;
        private CampusVM _campus;
        private CampusRoomVM _room;

        private ObservableCollection<InvoiceVM> _allInvoices;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public DateTime DeadendDate
        {
            get { return _deadendDate; }
            set
            {
                _deadendDate = value;
                NotifyPropertyChanged("DeadEndDate");
            }
        }
        public string Month
        {
            get {
                _month = MonthConverter.NoToMonthConverter(DeadendDate.Month);
                return _month; }
            set
            {
                _month = value;
                NotifyPropertyChanged("Month");
            }
        }

        public DateTime? DatePayed
        {
            get { return _datePayed; }
            set
            {
                _datePayed = value;
                NotifyPropertyChanged("DatePayed");
            }
        }
        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyPropertyChanged("Total");
            }
        }
        public bool IsPayed
        {
            get { return _isPayed; }
            set { _isPayed = value;
                NotifyPropertyChanged("IsPayed");
            }
        }
        public int IdStudent
        {
            get { return _idStudent; }
            set
            {
                _idStudent = value;
                NotifyPropertyChanged("IdStudent");
            }
        }
        public StudentVM Student
        {
            get {
                _student = _studAct.GetStudentById(IdStudent);
                return _student; }
            set
            {
                _student = value;
                NotifyPropertyChanged("Student");
            }
        }
        public StudentTypeVM StudentType
        {
            get
            {
                _studentType = Student.StudentType;
                return _studentType;
            }
            set
            {
                _studentType = value;
                NotifyPropertyChanged("StudentType");
            }
        }
        public UniversityVM University
        {
            get {
                _university = Student.University;
                return _university; }
            set
            {
                _university = value;
                NotifyPropertyChanged("University");
            }
        }
        public CampusVM Campus
        {
            get {
                _campus = Student.Campus;
                return _campus; }
            set
            {
                _campus = value;
                NotifyPropertyChanged("Campus");
            }
        }
        public CampusRoomVM Room
        {
            get {
                _room = Student.CampusRoom;
                return _room; }
            set
            {
                _room = value;
                NotifyPropertyChanged("Room");
            }
        }

        public ObservableCollection<InvoiceVM> AllInvoices
        {
            get {
                _allInvoices = _invoiceAct.AllInvoices(IdStudent);
                return _allInvoices; }
            set
            {
                _allInvoices = value;
                NotifyPropertyChanged("AllInvoices");
            }
        }

        #endregion
        #region Commands
        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(SearchStudent);
                }
                return searchCommand;
            }
        }
        private ICommand payCommand;
        public ICommand PayCommand
        {
            get
            {
                if (payCommand == null)
                {
                    payCommand = new RelayCommand(PayTax);
                }
                return payCommand;
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddInvoices);
                }
                return addCommand;
            }
        }
        #endregion
        public StudentVM GetStudent(string ssn)
        {
            if (ssn is null)
            {
                MessageBox.Show("No student found!");
                return null;
            }
            var student = _studAct.GetStudentBySSN(ssn);
            if (student is null)
                return null;
            return student;
        }
        public void SearchStudent(object obj)
        {
            string ssn = obj as string;
            var student = GetStudent(ssn);
            IdStudent = student.Id;
            Student = student;
            AllInvoices = _invoiceAct.AllInvoices(IdStudent);
        }
        public void PayTax(object obj)
        {
            string ssn = obj as string;
            var student = GetStudent(ssn);
            IdStudent = student.Id;
            Student = student;
            _invoiceAct.PayTax(IdStudent);
        }
        public void AddInvoices(object obj)
        {
            string ssn = obj as string;
            var student = GetStudent(ssn);
            _invoiceAct.AddInvoices(student.Id, 2);
        }
    }
}
