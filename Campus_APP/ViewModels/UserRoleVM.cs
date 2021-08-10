using Campus_APP.Helpers;

namespace Campus_APP.ViewModels
{
    class UserRoleVM:BaseVM
    {
        #region DataMembers
        private int _id;
        private string _name;

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
        #endregion
    }
}
