using Campus_APP.Helpers;
using Campus_APP.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Campus_APP.ViewModels.UXViewModels.AdminViewModels
{
    class MainAdminWindowVM:BaseVM
    {
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
            }
        }
    }
}
