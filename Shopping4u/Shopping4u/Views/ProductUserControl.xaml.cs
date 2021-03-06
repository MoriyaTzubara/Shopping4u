using BE;
using Shopping4u.BL;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Shopping4u.Extensions;

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public OrderedProductViewModel productViewModel;
        public ProductUserControl()
        {
            InitializeComponent();
        }
        public ProductUserControl(OrderedProductViewModel product)
        {
            InitializeComponent();
            productViewModel = product;
            DataContext = productViewModel;
            InitializeBrachesComboBox();
        }
        public void InitializeBrachesComboBox()
        {
            IBL bl = new BL.BL();
            foreach (BranchProduct branchProduct in bl.GetBranchProductsOfSpecificProduct(productViewModel.orderedProduct.GetProduct().id))
            {
                addBranchToComboBox(branchProduct, selected:bl.GetBranch(branchProduct.branchId).id == productViewModel.orderedProduct.GetBranch().id);
            }
        }
        private void addBranchToComboBox(BranchProduct branchProduct, bool selected=false)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = branchProduct.GetBranch().name;
            item.Tag = branchProduct.branchProductId;
            item.IsSelected = selected;

            //Branches.Items.Add(item);
        }

    }
}
