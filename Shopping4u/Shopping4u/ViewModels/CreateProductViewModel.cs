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

namespace Shopping4u.ViewModels
{
    public class CreateProductViewModel: IProductViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<BranchProductViewModel> branches;
        public ObservableCollection<BranchProductViewModel> Branches { get { return branches; } set { branches = value; OnPropertyChanged(); } }


        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products { get { return products; } set { products = value; OnPropertyChanged(); } }

        private string imgUrl;
        public string ImgUrl { get { return imgUrl; } set { imgUrl = value; OnPropertyChanged(); } }

        private int branchProductId;
        public int BranchProductId 
        { 
            get { return branchProductId; } 
            set 
            {
                branchProductId = value;
                OnPropertyChanged();
            }
        }

        internal void ShowProperBranches(int productId)
        {
            IBL bl = new BL.BL();
            Branches = new ObservableCollection<BranchProductViewModel>(bl.GetBranchProductsOfSpecificProduct(productId).Select(x=> new BranchProductViewModel(x)));
        }

        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged(); } }

        private string unitPrice;
        public string UnitPrice { get { return unitPrice; } set { unitPrice = value; OnPropertyChanged(); } }

        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }
        public SelectProductCommand SelectProductCommand { get; set; }
        public SelectBranchProductCommand SelectBranchProductCommand { get; set; }



        public CreateProductViewModel()
        {
            IBL bl = new BL.BL();
            Branches = new ObservableCollection<BranchProductViewModel>();
            Products = new ObservableCollection<Product>(bl.GetProducts());
            Quantity = 1;
            UnitPrice = "0$";

            UpdateQuantityCommand = new UpdateQuantityCommand(this);
            SelectProductCommand = new SelectProductCommand(this);
            SelectBranchProductCommand = new SelectBranchProductCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
