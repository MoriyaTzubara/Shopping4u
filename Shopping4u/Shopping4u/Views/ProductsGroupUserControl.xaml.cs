using Shopping4u.ViewModels;
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
using Shopping4u.ViewModels.Charts;

namespace Shopping4u.Views
{
    /// <summary>
    /// Interaction logic for ProductsGroupUserControl.xaml
    /// </summary>
    public partial class ProductsGroupUserControl : UserControl
    {
        public ProductsGroupUserControl(ProductsGroupViewModel productsGroupViewModel)
        {
            InitializeComponent();
            DataContext = productsGroupViewModel;
        }
    }
}
