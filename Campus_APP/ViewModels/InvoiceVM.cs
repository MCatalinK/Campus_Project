using Campus_APP.Helpers;
using System;

namespace Campus_APP.ViewModels
{
    class InvoiceVM:BaseVM
    {
        #region DataMemebers
        private int _id;
        private DateTime _emissionDate;
        private decimal _total;
        private int _idStudent;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public DateTime EmissionDate
        {
            get { return _emissionDate; }
            set
            {
                _emissionDate = value;
                NotifyPropertyChanged("EmissionDate");
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
        public int IdStudent
        {
            get { return _idStudent; }
            set
            {
                _idStudent = value;
                NotifyPropertyChanged("IdStudent");
            }
        }
        #endregion
    }
}
