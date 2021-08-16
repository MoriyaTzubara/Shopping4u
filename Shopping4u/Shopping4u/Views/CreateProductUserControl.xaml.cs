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
using Shopping4u.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shopping4u.Extensions;

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for CreateProductUserControl.xaml
    /// </summary>
    public partial class CreateProductUserControl : UserControl, INotifyPropertyChanged
    {
        public CreateProductViewModel CreateProductViewModel { get; set; }
        public CreateProductUserControl(CreateProductViewModel createProductViewModel)
        {
            InitializeComponent();
            CreateProductViewModel = createProductViewModel;
            DataContext = CreateProductViewModel;
            CreateProductViewModel.ProductSelectedEvent += SelectProduct;
            CreateProductViewModel.BranchProductSelectedEvent += SelectBranchProduct;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SelectBranchProduct(object sender, BranchProduct branchProduct)
        {
            this.Branches.SelectedItem = Branches.Items.OfType<BranchProductViewModel>().FirstOrDefault(p => p.branchProduct.branchProductId == branchProduct.branchProductId);
        }
        public void SelectProduct(object sender, Product product)
        {
            this.Products.SelectedItem = Products.Items.OfType<Product>().FirstOrDefault(p => p.id == product.id);
        }
    }
}
