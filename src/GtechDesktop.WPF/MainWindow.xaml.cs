using GtechDesktop.WPF.UserControls;
using System.Windows;

namespace GtechDesktop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new Home();
        }
    }
}
