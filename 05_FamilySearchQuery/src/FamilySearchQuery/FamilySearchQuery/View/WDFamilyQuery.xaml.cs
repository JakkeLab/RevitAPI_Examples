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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FamilySearchQuery.View
{
    /// <summary>
    /// Interaction logic for WDFamilyQuery.xaml
    /// </summary>
    public partial class WDFamilyQuery : Window
    {
        public WDFamilyQuery()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbParameterName_Checked(object sender, RoutedEventArgs e)
        {
            tbParameterName.IsEnabled = true;
        }

        private void cbParameterName_Unchecked(object sender, RoutedEventArgs e)
        {
            tbParameterName.IsEnabled = false;
        }

        private void cbParameterValue_Checked(object sender, RoutedEventArgs e)
        {
            tbParameterValue.IsEnabled = true;
        }

        private void cbParameterValue_Unchecked(object sender, RoutedEventArgs e)
        {
            tbParameterValue.IsEnabled = false;
        }
    }
}
