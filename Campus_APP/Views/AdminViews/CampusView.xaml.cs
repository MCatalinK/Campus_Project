using Campus_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Campus_APP.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for CampusView.xaml
    /// </summary>
    public partial class CampusView : Window
    {
        public CampusView()
        {
            InitializeComponent();
        }

        private void cbUni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (cbUni.SelectedItem as UniversityVM).Id;
            (DataContext as CampusVM).Load(item);
        }
    }
}
