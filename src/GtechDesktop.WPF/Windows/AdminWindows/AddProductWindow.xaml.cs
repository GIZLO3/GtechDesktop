using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        private string ImageSource;
        private string ImageFileName;
        private string ImageSafeFileName;
        private Dictionary<string, TextBox> parametersTextBoxes = new Dictionary<string, TextBox>();
        public AddProductWindow()
        {
            InitializeComponent();
            GetCategories();
            GetProducers();
        }

        private void GetCategories()
        {
            var categories = CategoryRepository.GetAllCategories();
            var categoryIdName = new List<string>();
            categories.ForEach(category => categoryIdName.Add(category.Id + ";" + category.Name));
            CategoriesCmbBox.ItemsSource = categoryIdName;
            CategoriesCmbBox.SelectedIndex = 0;
        }

        private void UpdateSubcategories(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            if(selectedItem != null)
            {
                var categoryId = int.Parse(selectedItem.Split(';')[0]);
                var subcategories = SubcategoryRepository.GetSubcategoriesByCategoryId(categoryId);
                var subcategoryIdName = new List<string>();
                subcategories.ForEach(subcategory => subcategoryIdName.Add(subcategory.Id + ";" + subcategory.Name));
                SubcategoriesCmbBox.ItemsSource = subcategoryIdName;
                SubcategoriesCmbBox.SelectedIndex = 0;
            }
        }

        private void GetProducers()
        {
            var producers = ProducerRepository.GetAllProducers();
            var producersIdName = new List<string>();
            producers.ForEach(producer => producersIdName.Add(producer.Id + ";" + producer.Name));
            ProducerCmbBox.ItemsSource = producersIdName;
            ProducerCmbBox.SelectedIndex = producersIdName.Count()-1;
        }

        private void ProducerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            ProducerId = int.Parse(selectedItem.Split(';')[0]);
        }

        private void UpdateParameters(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            parametersTextBoxes = new Dictionary<string, TextBox>();
            if (selectedItem != null)
            {
                SubcategoryId = int.Parse(selectedItem.Split(';')[0]);
                ParametersGrid.Children.Clear();
                ParametersGrid.RowDefinitions.Clear();
                var subcategory = SubcategoryRepository.GetSubcategory(SubcategoryId);
                var parameters = subcategory.ParametersPatten;

                var parameterIndex = 0;
                for (var i = 0; i <= (parameters.Count / 2); i++)
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
                        //textBox.SetValue(FrameworkElement.NameProperty, parameters[parameterIndex]);
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

        private void AddImageButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Wybierz zdjęcie";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if(dialog.ShowDialog() == true)
            {
                ImageFileName = dialog.FileName;
                ImageSafeFileName = dialog.SafeFileName;
                ImageSource = Path.Combine(@"\Images", dialog.SafeFileName);
                Image.Source = new BitmapImage(new Uri(ImageFileName));
            }    
        }

        private void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(NameTxt.Text) || string.IsNullOrEmpty(PriceTxt.Text) || string.IsNullOrEmpty(AmountTxt.Text))
            {
                MessageBox.Show("Uzupełnij wszytkie pola po lewej stonie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(PriceTxt.Text.All(char.IsDigit) && AmountTxt.Text.All(char.IsDigit))
            {
                var product = new Product();
                product.Name = NameTxt.Text;
                product.Price = decimal.Parse(PriceTxt.Text);
                product.Amount = int.Parse(AmountTxt.Text);
                var newDirectory = Path.Combine(@"..\..\..\Images", ImageSafeFileName);
                File.Copy(ImageFileName, newDirectory);
                product.Image = product.Image = ImageSource;

                var properties = new Dictionary<string, string>();
                var keys = parametersTextBoxes.Keys;
                foreach (var key in keys)
                {
                    properties.Add(key, parametersTextBoxes[key].Text);
                }
                product.Properties = properties;

                product.ProducerId = ProducerId;
                product.SubcategoryId = SubcategoryId;

                ProductRepository.InsertProduct(product);
                MessageBox.Show("Dodano produkt!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
                MessageBox.Show("Cena lub ilośc jest niepoprawna!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddProducerButtonClick(object sender, RoutedEventArgs e)
        {
            var addProducerWindow = new AddProducer();
            addProducerWindow.ShowDialog();
            GetProducers();
        }
    }
}
