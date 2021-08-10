using Campus_APP.Helpers;
using Campus_APP.Models.Actions;

namespace Campus_APP.ViewModels
{
    class UserVM:BaseVM
    {
        private readonly UserRoleActions _roleAct;

        public UserVM()
        {
            _roleAct = new UserRoleActions();
        }

        #region DataMembers
        private int _id;
        private string _username;
        private string _password;
        private int _idRole;
        private UserRoleVM _role;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }
        public int IdRole
        {
            get { return _idRole; }
            set { _idRole = value;
                NotifyPropertyChanged("IdRole");
            }
        }
        public UserRoleVM Role
        {
            get
            {
                _role = _roleAct.GetRole(IdRole);
                return _role;
            }
            set
            {
                _role = value;
                NotifyPropertyChanged("Role");
            }
        }
        #endregion
    }
}
