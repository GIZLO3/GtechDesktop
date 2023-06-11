using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private int SubcategoryId;
        private int ProducerId;
        private string? ImageSource;
        private string? ImageFileName = null;
        private string? ImageSafeFileName;
        private Dictionary<string, TextBox> parametersTextBoxes = new Dictionary<string, TextBox>();
        public AddProductWindow()
        {
            InitializeComponent();

            GetCategories();
            GetProducers();
        }
        private void GetCategories()//pobranie kategorii z bazy i wstawienie do combobox
        {
            var categories = CategoryRepository.GetAllCategories();
            var categoryIdName = new List<string>();
            categories.ForEach(category => categoryIdName.Add(category.Id + ";" + category.Name));//zapisanie kategorii do listy w postaci id i nazwy rodzielonymi separatorem ;
            CategoriesCmbBox.ItemsSource = categoryIdName;
            CategoriesCmbBox.SelectedIndex = 0;
        }

        private void UpdateSubcategories(object sender, SelectionChangedEventArgs e)//obsługa zmiany zaznaczenia kategorii
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            if(selectedItem != null)
            {
                var categoryId = int.Parse(selectedItem.Split(';')[0]);//pobranie id kategorii z zaznaczonego elementu z listy kategorii
                var subcategories = SubcategoryRepository.GetSubcategoriesByCategoryId(categoryId);//pobranie podkategorii z danej kategorii z bazy 
                var subcategoryIdName = new List<string>();
                subcategories.ForEach(subcategory => subcategoryIdName.Add(subcategory.Id + ";" + subcategory.Name));//zapisanie podkategorii do listy w postaci id i nazwy rodzielonymi separatorem ;
                SubcategoriesCmbBox.ItemsSource = subcategoryIdName;
                SubcategoriesCmbBox.SelectedIndex = 0;
            }
        }
        private void GetProducers()//pobranie producentów z bazy
        {
            var producers = ProducerRepository.GetAllProducers();
            var producersIdName = new List<string>();
            producers.ForEach(producer => producersIdName.Add(producer.Id + ";" + producer.Name));//zapisanie producenta do listy w postaci id i nazwy rodzielonymi separatorem ;
            ProducerCmbBox.ItemsSource = producersIdName;
            ProducerCmbBox.SelectedIndex = producersIdName.Count()-1;
        }

        private void ProducerSelectionChanged(object sender, SelectionChangedEventArgs e)//przy zmianie producenta, zapisanie jego id w polu ProducerId
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            if(selectedItem != null)
                ProducerId = int.Parse(selectedItem.Split(';')[0]);
        }

        private void UpdateParameters(object sender, SelectionChangedEventArgs e)//obsługa zmienienia zaznaczenia podkategorii
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            parametersTextBoxes = new Dictionary<string, TextBox>();//utworzenie dziennika z nazwami i TextBoxami parametrów
            if (selectedItem != null)
            {
                SubcategoryId = int.Parse(selectedItem.Split(';')[0]);
                ParametersGrid.Children.Clear();//wyczyszczenie grida z parametrami
                ParametersGrid.RowDefinitions.Clear();
                var subcategory = SubcategoryRepository.GetSubcategory(SubcategoryId);//pobranie podkategorii z bazy, aby mieć aktualne parametry
                var parameters = subcategory.ParametersPattern;

                if(parameters != null)
                {
                    var parameterIndex = 0;
                    for (var i = 0; i <= (parameters.Count / 2); i++)//wstawianie textboxów z parametrami w 2 kolumnach
                    {
                        ParametersGrid.RowDefinitions.Add(new RowDefinition());
                        for (var j = 0; j < 2; j++)
                        {
                            if (parameterIndex >= parameters.Count)
                                break;

                            var stackPanel = new StackPanel();
                            var textBox = new TextBox();
                            var label = new Label();
                            label.Content = parameters[parameterIndex] + ": ";
                            label.FontWeight = FontWeights.Bold;
                            parametersTextBoxes.Add(parameters[parameterIndex], textBox);
                            parameterIndex++;

                            stackPanel.SetValue(Grid.RowProperty, i);
                            stackPanel.SetValue(Grid.ColumnProperty, j);
                            stackPanel.VerticalAlignment = VerticalAlignment.Center;

                            stackPanel.Children.Add(label);
                            stackPanel.Children.Add(textBox);

                            ParametersGrid.Children.Add(stackPanel);
                        }
                    }
                }
            }
        }

        private void AddImageButtonClick(object sender, RoutedEventArgs e)//dodawanie zdjęcia
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Wybierz zdjęcie";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                ImageFileName = dialog.FileName;
                ImageSafeFileName = dialog.SafeFileName;
                ImageSource = Path.Combine(@"\Images", dialog.SafeFileName);
                Image.Source = new BitmapImage(new Uri(ImageFileName));//podgląd zdjęcia
            }
        }

        private void AddProductButtonClick(object sender, RoutedEventArgs e)//dodawanie produktu
        {
            var priceRegex = new Regex(@"^\d*\,?\d*$");

            if(string.IsNullOrEmpty(NameTxt.Text) || string.IsNullOrEmpty(PriceTxt.Text) || string.IsNullOrEmpty(AmountTxt.Text) || ImageFileName == null)
            {
                MessageBox.Show("Uzupełnij wszytkie pola po lewej stonie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (priceRegex.IsMatch(PriceTxt.Text) && AmountTxt.Text.All(char.IsDigit) && ImageSafeFileName != null)
            {
                var product = new Product();
                product.Name = NameTxt.Text;
                product.Price = decimal.Parse(PriceTxt.Text);
                product.Amount = int.Parse(AmountTxt.Text);
                var newDirectory = Path.Combine(@"..\..\..\Images", ImageSafeFileName);
                File.Copy(ImageFileName, newDirectory);//kopiowanie zdjęcia do folderu w Images
                product.Image = ImageSource;
                
                var properties = new Dictionary<string, string>();
                var keys = parametersTextBoxes.Keys;
                foreach (var key in keys)//odczytywanie wartości z każdego texboxa z parametrami
                {
                    properties.Add(key, parametersTextBoxes[key].Text);
                }
                product.Properties = properties;

                product.ProducerId = ProducerId;
                product.SubcategoryId = SubcategoryId;

                ProductRepository.InsertProduct(product);//dodanie produktu do bazy
                MessageBox.Show("Dodano produkt!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
                MessageBox.Show("Cena lub ilośc jest niepoprawna!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddProducerButtonClick(object sender, RoutedEventArgs e)//otworzenie okna dodawania producenta
        {
            var addProducerWindow = new AddProducer();
            addProducerWindow.ShowDialog();
            GetProducers();
        }
    }
}
