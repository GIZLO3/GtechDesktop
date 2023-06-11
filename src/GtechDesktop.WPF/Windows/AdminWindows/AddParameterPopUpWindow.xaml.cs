using System.Windows;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for AddParameterPopUpWindow.xaml
    /// </summary>
    public partial class AddParameterPopUpWindow : Window
    {
        public string Parameter //pole Parametr, dzięki któremu możemy zwrócić wartość pola tekstowego do miejsca wywołania metody ShowDialog okna
        { 
            get { return ParameterTxt.Text; } 
        }

        public AddParameterPopUpWindow()
        {
            InitializeComponent();
        }

        private void AddParameterButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ParameterTxt.Text))
            {
                DialogResult = true;//Ustawnienie DialogResult na true
                Close();
            }
            else
                MessageBox.Show("Uzupełnij nazwę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
