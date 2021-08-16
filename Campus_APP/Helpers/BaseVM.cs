using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Campus_APP.Helpers
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());
    }
}
