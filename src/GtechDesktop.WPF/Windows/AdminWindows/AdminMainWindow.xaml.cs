using System.Windows;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }

        private void ProductsManagmentButtonClick(object sender, RoutedEventArgs e)
        {
            var productsManagmentWindow = new ProductsManagmentWindow();
            productsManagmentWindow.ShowDialog();
        }

        private void AddSubcategoryButtonClick(object sender, RoutedEventArgs e)
        {
            var addSubcategoryWindow = new AddSubcategoryWindow();
            addSubcategoryWindow.ShowDialog();
        }

        private void SubcategoryManagmentButtonClick(object sender, RoutedEventArgs e)
        {
            var subcategoriesManagmentWindow = new SubcategoriesManagmentWindow();
            subcategoriesManagmentWindow.ShowDialog();
        }
        private void UsersManagmentButtonClick(object sender, RoutedEventArgs e)
        {
            var usersManagmentWindow = new UsersManagmentWindow();
            usersManagmentWindow.ShowDialog();
        }
    }
}
