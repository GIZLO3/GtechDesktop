using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;

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

        private void AddCategoryButtonClick(object sender, RoutedEventArgs e)//dodanie kategorii
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
