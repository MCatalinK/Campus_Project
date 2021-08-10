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
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        public StudentView()
        {
            InitializeComponent();
        }

        private void cbUni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (cbUni.SelectedItem as UniversityVM).Id;
            (DataContext as StudentVM).UniChanged(id);
        }

        private void cbCampus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (cbCampus.SelectedItem as CampusVM).Id;
            (DataContext as StudentVM).CampusChanged(id);
        }

    }
}
