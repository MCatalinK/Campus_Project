using Campus_APP.Helpers;
using Campus_APP.Models.Actions;
using Campus_APP.Views;
using System.Windows.Input;

namespace Campus_APP.ViewModels.UXViewModels
{
    class MainWindowVM:BaseVM
    {

        private readonly StudentTypeActions _typeAct;

        public MainWindowVM()
        {
            _typeAct = new StudentTypeActions();
            _typeAct.SetTypes();
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

        public void OpenWindow(object obj)
        {
            string option = obj as string;
            switch (option)
            {
                case "1":
                    UniversityView universityView = new UniversityView();
                    universityView.ShowDialog();
                    break;
                case "2":
                    CampusView campusView = new CampusView();
                    campusView.ShowDialog();
                    break;
                case "3":
                    StudentView studentView = new StudentView();
                    studentView.ShowDialog();
                    break;
                case "4":
                    InvoiceView invoiceView = new InvoiceView();
                    invoiceView.ShowDialog();
                    break;
            }
        }
    }
}
