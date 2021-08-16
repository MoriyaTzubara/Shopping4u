using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.ViewModels;
using Shopping4u.Extensions;

namespace Shopping4u.ViewModels
{
    public class CreateProductViewModel: IProductViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public CreateProductViewModel()
        {
            IBL bl = new BL.BL();

            Branches = new ObservableCollection<BranchProductViewModel>();
            Products = new ObservableCollection<Product>(bl.GetProducts());
            
            UpdateQuantityCommand = new UpdateQuantityCommand(this);
            SelectProductCommand = new SelectProductCommand(this);
            SelectBranchProductCommand = new SelectBranchProductCommand(this);
            
            ImgUrl = "";
            Quantity = 0;
            UnitPrice = 0;

        }

        public ObservableCollection<BranchProductViewModel> Branches { get; set; }
        public ObservableCollection<Product> Products { get; set; }


        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }
        public SelectProductCommand SelectProductCommand { get; set; }
        public SelectBranchProductCommand SelectBranchProductCommand { get; set; }

        private string imgUrl;
        public string ImgUrl 
        { 
            get { return imgUrl; } 
            set { imgUrl = value; OnPropertyChanged(); } 
        }

        private int branchProductId;
        public int BranchProductId 
        { 
            get { return branchProductId; } 
            set { branchProductId = value; OnPropertyChanged(); }
        }
        
        private int quantity;
        public int Quantity
        { 
            get { return quantity; } 
            set { 
                quantity = value; 
                OnPropertyChanged(); 
            } 
        }

        private double unitPrice;
        public double UnitPrice 
        { 
            get { return unitPrice; } 
            set { unitPrice = value; OnPropertyChanged(); }
        }


        public bool CanScanQRCode { get; set; }
        public bool CanSaveShoppingList { get; set; }
        public string ScanQRCodeVisibility
        {
            get => CanScanQRCode ? "Visible" : "Collapsed";
            private set { }
        }
        public string SaveShoppingListVisibility
        {
            get => CanSaveShoppingList ? "Visible" : "Collapsed";
            private set { }
        }

        private void showProperBranches(int productId)
        {
            IBL bl = new BL.BL();
            Branches = new ObservableCollection<BranchProductViewModel>(bl.GetBranchProductsOfSpecificProduct(productId).Select(x=> new BranchProductViewModel(x)));
            OnPropertyChanged("Branches");
        }

        public void ProductSelected(Product selectedProduct)
        {
            ImgUrl = selectedProduct.imageUrl;
            Quantity = 1;
            UnitPrice = 0;
            showProperBranches(selectedProduct.id);
        }

        public void BranchProductSelected(BranchProduct selectedBranchProduct)
        {
            BranchProductId = selectedBranchProduct.branchProductId;
            UnitPrice = selectedBranchProduct.price;
            Quantity = 1;
        }
        public void ScannedProduct(OrderedProduct orderedProduct)
        {
            ProductSelected(orderedProduct.GetProduct());
            BranchProductSelected(orderedProduct.GetBranchProduct());
            //PropertyChanged = new PropertyChangedEventHandler()
            //ImgUrl = orderedProductViewModel.ImgUrl;
            //Quantity = orderedProductViewModel.Quantity;
            //UnitPrice = orderedProductViewModel.UnitPrice;
        }
    }
}
