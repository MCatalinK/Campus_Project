using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using Campus_APP.Views.AdminViews;
using System.Windows;
using System.Windows.Input;

namespace Campus_APP.ViewModels.UXViewModels
{
    class LoginVM:BaseVM
    {
        readonly UserRoleActions _rolesAct;
        readonly StudentTypeActions _typesAct;
        readonly UserActions _userAct;
        public LoginVM()

        {
            _rolesAct = new UserRoleActions();
            _rolesAct.SetRoles();

            _typesAct = new StudentTypeActions();
            _typesAct.SetTypes();

            _userAct = new UserActions();
        }

        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }

        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());

        public void OpenWindow(object obj)
        {
            
            UserVM user = obj as UserVM;
            user = _userAct.GetUser(user);
            if (user is null) return;
            switch (user.IdRole)
            {
                case 1:
                    MainAdminView mainAdmin = new MainAdminView();
                    mainAdmin.ShowDialog();
                    break;
                case 2:
                    break;
            }
            
        }
    }
}
