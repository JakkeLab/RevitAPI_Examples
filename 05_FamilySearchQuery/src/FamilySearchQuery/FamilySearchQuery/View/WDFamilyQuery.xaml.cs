using Autodesk.Revit.UI;
using FamilySearchQuery.MyExternalEvent;
using FamilySearchQuery.ViewModel;
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
        public SearchViewModel ViewModel { get; set; }
        public WDFamilyQuery()
        {
            InitializeComponent();
            ViewModel = new SearchViewModel();
            ViewModel.CurrentMode = SearchMode.CategoryOnly;
            cmbCategory.SelectedIndex = 0;
            DataContext = ViewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbParameterName_Checked(object sender, RoutedEventArgs e)
        {
            tbParameterName.IsEnabled = true;
            cbParameterValue.IsEnabled = true;
            ViewModel.CurrentMode = SearchMode.CategoryAndParameterExist;
        }

        private void cbParameterName_Unchecked(object sender, RoutedEventArgs e)
        {
            tbParameterName.IsEnabled = false;
            tbParameterName.Text = string.Empty;

            cbParameterValue.IsEnabled = false;
            cbParameterValue.IsChecked = false;
            tbParameterValue.IsEnabled = false;
            tbParameterValue.Text = string.Empty;
            ViewModel.CurrentMode = SearchMode.CategoryOnly;
        }

        private void cbParameterValue_Checked(object sender, RoutedEventArgs e)
        {
            tbParameterValue.IsEnabled = true;
            ViewModel.CurrentMode = SearchMode.CategoryAndParameterValue;
        }

        private void cbParameterValue_Unchecked(object sender, RoutedEventArgs e)
        {
            tbParameterValue.IsEnabled = false;
            tbParameterValue.Text = string.Empty;
            ViewModel.CurrentMode = SearchMode.CategoryAndParameterExist;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //뷰모델에 값 저장
            ViewModel.TargetParameterName = tbParameterName.Text;
            ViewModel.TargetParameterValue = tbParameterValue.Text;

            this.ViewModel.ExternealEventInstance.Raise();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.CurrentCategoryName = cmbCategory.SelectedItem as string;
        }
    }
}
