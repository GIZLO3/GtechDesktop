using GtechDesktop.WPF.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GtechDesktop.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for SubactegoryList.xaml
    /// </summary>
    public partial class SubactegoryList : UserControl
    {
        public SubactegoryList(Subcategory subcategory)
        {
            InitializeComponent();

            foreach(var paremeter in subcategory.ParametersPatten)
            {
                MessageBox.Show(paremeter, "1", MessageBoxButton.OK);
            }
        }
    }
}
