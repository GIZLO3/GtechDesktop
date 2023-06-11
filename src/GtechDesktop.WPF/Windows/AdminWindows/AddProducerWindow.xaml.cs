using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;

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

        private void AddProducerButtonClick(object sender, RoutedEventArgs e)//dodanie producenta
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
