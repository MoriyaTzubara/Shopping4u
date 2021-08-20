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

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for ShoppingListPage.xaml
    /// </summary>
    public partial class ShoppingListPage : UserControl
    {   
        public ShoppingListPage(ShoppingListViewModel shoppingListViewModel)
        {
            InitializeComponent();
            DataContext = shoppingListViewModel;

            CreateProductCard.Child = new CreateProductUserControl(shoppingListViewModel.CreateProductViewModel);
        }

    }
}
