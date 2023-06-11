using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for AddSubcategoryWindow.xaml
    /// </summary>
    public partial class AddSubcategoryWindow : Window
    {
        private Subcategory? Subcategory { get; set; }
        private List<string> ParametersPattern = new List<string>();
        public AddSubcategoryWindow()//kostruktor do dodawania
        {
            InitializeComponent();

            Title = "Dodaj podkategorię";
            TitleLabel.Content = "Dodaj podkategorię";
            SubmitButton.Content = "Dodaj podkategorię";
            SubmitButton.Click += AddSubcategoryButtonClick;
            GetCategories();
            ParametersDataGrid.ItemsSource = ParametersPattern;
        }
        public AddSubcategoryWindow(Subcategory subcategory)//kostruktor do edycji
        {
            InitializeComponent();

            Subcategory = subcategory;
            Title = "Edytuj podkategorię";
            TitleLabel.Content = "Edytuj podkategorię";
            SubmitButton.Content = "Edytuj podkategorię";
            SubmitButton.Click += EditSubcategoryButtonClick;
            GetCategories();
            MessageBox.Show("Sprawdź ustawioną kategorię!", "G-tech", MessageBoxButton.OK, MessageBoxImage.Warning);
            NameTxt.Text = subcategory.Name;
            if(subcategory.ParametersPattern != null)
                ParametersPattern = subcategory.ParametersPattern;
            ParametersDataGrid.ItemsSource = ParametersPattern;
        }

        private void GetCategories() //pobranie kategorii z bazy i wstawienie do combobox
        {
            var categories = CategoryRepository.GetAllCategories();
            var categoryIdName = new List<string>();
            categories.ForEach(category => categoryIdName.Add(category.Id + ";" + category.Name));
            CategoriesCmbBox.ItemsSource = categoryIdName;
            CategoriesCmbBox.SelectedIndex = categoryIdName.Count()-1;
        }

        private void AddCategoryButtonClick(object sender, RoutedEventArgs e)//otworzenie okna dodawania kategorii
        {
            var addCategoryWindow = new AddCategoryWindow();
            addCategoryWindow.ShowDialog();
        }

        private void AddParameterButtonClick(object sender, RoutedEventArgs e)//otworzenie okna dodawania parametru
        {
            var addParameterPopUpWindow = new AddParameterPopUpWindow();
            if(addParameterPopUpWindow.ShowDialog() == true)
            {
                ParametersPattern.Add(addParameterPopUpWindow.Parameter);
                ParametersDataGrid.Items.Refresh();
            }
        }

        private void DeleteParametrButtonClick(object sender, RoutedEventArgs e)//usuwanie parametru
        {
            ParametersPattern.RemoveAt(ParametersDataGrid.SelectedIndex);
            ParametersDataGrid.Items.Refresh();
        }

        private void AddSubcategoryButtonClick(object sender, RoutedEventArgs e)//dodawanie podkategroii do bazy
        {
            var selectedCategory = CategoriesCmbBox.SelectedItem as string;
            if(selectedCategory != null && !string.IsNullOrEmpty(NameTxt.Text))
            {
                var subcategory = new Subcategory();
                subcategory.Name = NameTxt.Text;
                subcategory.ParametersPattern = ParametersPattern;
                subcategory.CategoryId = int.Parse(selectedCategory.Split(';')[0]);
                SubcategoryRepository.InsertSubcategory(subcategory);
                Close();
            }
        }
        private void EditSubcategoryButtonClick(object sender, RoutedEventArgs e)//edycja podkategorii w bazie
        {
            var selectedCategory = CategoriesCmbBox.SelectedItem as string;
            if (selectedCategory != null && !string.IsNullOrEmpty(NameTxt.Text) && Subcategory != null)
            {
                Subcategory.Name = NameTxt.Text;
                Subcategory.ParametersPattern = ParametersPattern;
                Subcategory.CategoryId = int.Parse(selectedCategory.Split(';')[0]);
                SubcategoryRepository.UpdateSubcategory(Subcategory);
                Close();
            }
        }
    }
}
