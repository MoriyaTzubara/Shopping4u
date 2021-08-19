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
using System.Windows.Forms;

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
            ScanQRCodeCommand = new ScanQRCodeCommand(this);
            SlecteImgCommand = new SlecteImgCommand(this);
            SaveImageProductCommand = new SaveImageProductCommand(this);

            ImgUrl = "";
            Quantity = 0;
            UnitPrice = 0;

        }

        public async void SaveImageProduct(string imgUrl, int branchProductId)
        {
            IBL bl = new BL.BL();
            Product product = bl.GetBranchProduct(branchProductId).GetProduct();
            await BL.BL.StorePicture(imgUrl, product.name, product.id);
            ImgUrl = bl.GetBranchProduct(branchProductId).GetProduct().imageUrl;
        }

        public ObservableCollection<BranchProductViewModel> Branches { get; set; }
        public ObservableCollection<Product> Products { get; set; }


        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }
        public SelectProductCommand SelectProductCommand { get; set; }
        public SelectBranchProductCommand SelectBranchProductCommand { get; set; }
        public ScanQRCodeCommand ScanQRCodeCommand { get; set; }
        public SlecteImgCommand SlecteImgCommand { get; set; }
        public SaveImageProductCommand SaveImageProductCommand { get; set; }

        public event EventHandler<BranchProduct> BranchProductSelectedEvent;
        public event EventHandler<Product> ProductSelectedEvent;

        public OrderedProduct orderedProduct { 
            get
            {
                return new OrderedProduct()
                {
                    branchProductId = this.BranchProductId,
                    quantity = this.Quantity,
                    unitPrice = this.UnitPrice
                };
            }
            set
            {

            }
        }

        private string imgUrl;
        public string ImgUrl 
        { 
            get {
                if (imgUrl == null || imgUrl == "")
                    return "\\Img\\default-img.png";
                return imgUrl;
            } 
            set { 
                imgUrl = value;
                OnPropertyChanged(); 
            } 
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

        private BranchProduct selectedBranchProduct;
        public BranchProduct SelectedBranchProduct
        { 
            get { return selectedBranchProduct; } 
            set { selectedBranchProduct = value; OnPropertyChanged(); }
        }

        private Product selectedProduct;
        public  Product SelectedProduct
        { 
            get { return selectedProduct; } 
            set { selectedProduct = value; OnPropertyChanged(); }
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

        public void showProperBranches(int productId)
        {
            IBL bl = new BL.BL();
            Branches = new ObservableCollection<BranchProductViewModel>(bl.GetBranchProductsOfSpecificProduct(productId).Select(x=> new BranchProductViewModel(x)));
            OnPropertyChanged("Branches");
        }

        public void ProductSelected(Product selectedProduct)
        {
            if (selectedProduct == null)
                return;
            ImgUrl = selectedProduct.imageUrl;
            Quantity = 1;
            UnitPrice = 0;
            SelectedProduct = selectedProduct;
            showProperBranches(selectedProduct.id);
            ProductSelectedEvent(this, selectedProduct);
        }

        public void BranchProductSelected(BranchProduct selectedBranchProduct)
        {
            if (selectedBranchProduct == null)
                return;
            BranchProductId = selectedBranchProduct.branchProductId;
            UnitPrice = selectedBranchProduct.price;
            Quantity = 1;
            SelectedBranchProduct = selectedBranchProduct;
            BranchProductId = selectedBranchProduct.branchProductId;
            BranchProductSelectedEvent(this, selectedBranchProduct);
        }
        public void ScannedProduct(OrderedProduct orderedProduct)
        {
            ProductSelected(orderedProduct.GetProduct());
            BranchProductSelected(orderedProduct.GetBranchProduct());
        }
        public OrderedProduct EncodeBarcode(string imgUrl)
        {
            IBL bl = new BL.BL();
            return bl.EncodeOrderedProductString(bl.EncodeBarcode(imgUrl));
        }
        public void ScanQRCode()
        {
            string imgUrl = "";
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == DialogResult.OK)
            {
                imgUrl = of.FileName;
            }
            if (imgUrl == "")
                return;

            //MessageBox.Show($"SCAN QR Code {imgUrl}");

            OrderedProduct orderedProduct = EncodeBarcode(imgUrl);
            ScannedProduct(orderedProduct);
        }

        public void SlectedImage()
        {
            string imgUrl = "";
            using (OpenFileDialog of = new OpenFileDialog())
            {
                //For any other formats
                of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    imgUrl = of.FileName;
                }
            }
            if (imgUrl == "")
                return;
            SaveImageProduct(imgUrl,branchProductId);
        }
    }
}
