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
    /// Interaction logic for AddProducer.xaml
    /// </summary>
    public partial class AddProducer : Window
    {
        public AddProducer()
        {
            InitializeComponent();
        }

        private void AddProducerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                var producer = new Producer();
                producer.Name = NameTxt.Text;
                ProducerRepository.InsertProducer(producer);
                Close();
            }
            else
                MessageBox.Show("Uzupełnij nazwę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
