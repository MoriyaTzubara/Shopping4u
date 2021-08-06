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
        public ProductViewModel productViewModel;
        public ProductUserControl()
        {
            InitializeComponent();
        }
        public ProductUserControl(ProductViewModel product)
        {
            InitializeComponent();
            productViewModel = product;
            DataContext = productViewModel;
            InitializeBrachesComboBox();
        }
        public void InitializeBrachesComboBox()
        {
            IBL bl = new BL.BL();
            foreach (Branch branch in bl.GetBranches())
            {
                addBranchToComboBox(branch, selected:branch.id == productViewModel.orderedProduct.GetBranch().id);
            }
        }
        private void addBranchToComboBox(Branch branch, bool selected=false)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = branch.name;
            item.Tag = branch.id;
            item.IsSelected = selected;

            Branches.Items.Add(item);
        }

    }
}
