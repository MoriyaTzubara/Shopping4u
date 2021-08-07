using BE;
using Shopping4u.BL;
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
    /// Interaction logic for CreateProductUserControl.xaml
    /// </summary>
    public partial class CreateProductUserControl : UserControl
    {
        public CreateProductUserControl()
        {
            InitializeComponent();
            InitializeProductsComboBox();
        }
        public void InitializeBrachesComboBox()
        {
            IBL bl = new BL.BL();
            foreach (Branch branch in bl.GetBranches())
            {
                addBranchToComboBox(branch);
            }
        }
        private void addBranchToComboBox(Branch branch)
        {
            //TO DO - show only the relevant branches  
            ComboBoxItem item = new ComboBoxItem();
            item.Content = branch.name;
            item.Tag = branch.id;

            Branches.Items.Add(item);
        }
        private void InitializeProductsComboBox()
        {
            IBL bl = new BL.BL();
            foreach (var product in bl.GetProducts())
            {
                addProductToComboBox(product);
            }
        }
        private void addProductToComboBox(Product product)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = $"{product.name} ({product.category})";
            item.Tag = product.id;

            Products.Items.Add(item);
        }
    }
}
