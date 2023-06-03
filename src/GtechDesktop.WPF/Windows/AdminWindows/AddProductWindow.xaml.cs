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
        private int CategoryId;
        private int SubcategoryId;
        private int ProducerId;
        private string ImageSource;
        private List<TextBox> parametersTextBoxes = new List<TextBox>();
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
                CategoryId = int.Parse(selectedItem.Split(';')[0]);
                var subcategories = SubcategoryRepository.GetSubcategoriesByCategoryId(CategoryId);
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
            ProducerCmbBox.SelectedIndex = 0;
        }

        private void ProducerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
            ProducerId = int.Parse(selectedItem.Split(';')[0]);
        }

        private void UpdateParameters(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as string;
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
                        label.Content = parameters[parameterIndex++] + ": ";
                        label.FontWeight = FontWeights.Bold;

                        stackPanel.SetValue(Grid.RowProperty, i);
                        stackPanel.SetValue(Grid.ColumnProperty, j);
                        stackPanel.VerticalAlignment = VerticalAlignment.Center;

                        stackPanel.Children.Add(label);
                        stackPanel.Children.Add(textBox);

                        ParametersGrid.Children.Add(stackPanel);

                        parametersTextBoxes.Add(textBox);
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
                var newDirectory = Path.Combine(@"..\..\..\Images", dialog.SafeFileName);
                File.Copy(dialog.FileName, newDirectory, true);
                ImageSource = Path.Combine(@"\Images", dialog.SafeFileName);
                ImageUrlLabel.Content = ImageSource;
            }    
        }
    }
}
