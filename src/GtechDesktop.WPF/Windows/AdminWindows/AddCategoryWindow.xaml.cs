using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
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

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        private void AddCategoryButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                var category = new Category();
                category.Name = NameTxt.Text;
                CategoryRepository.InsertCategory(category);
                Close();
            }
            else
                MessageBox.Show("Uzupełnij nazwę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error); 
        }
    }
}
