using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;

namespace Shopping4u.ViewModels
{
    public abstract class ShoppingListViewModel : INotifyPropertyChanged
    {

        public ShoppingListModel ShoppingListModel;

        public ShoppingListViewModel(ShoppingListModel shoppingListModel)
        {
            ShoppingListModel = shoppingListModel;

            Products = new ObservableCollection<ProductViewModel>(shoppingListModel.GetProducts().Select(x => new ProductViewModel(x)));
            
            CreateProductCommand = new CreateProductCommand(this);
            UpdateProductCommand = new UpdateProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);

            ShowCreateProductCommand = new ShowCreateProductCommand(this);

            ProductConverter = new ProductConverter();
            IsShowCreateProduct = false;
        }


        public ShowCreateProductCommand ShowCreateProductCommand { get; set; }
        public CreateProductCommand CreateProductCommand { get; set; }
        public UpdateProductCommand UpdateProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }

        public ProductConverter ProductConverter { get; set; }

        public void ShowCreateProduct(bool isShow)
        {
            IsShowCreateProduct = isShow;
        }


        public CreateProductViewModel CreateProductViewModel;

        private bool isShowCreateProduct;
        public bool IsShowCreateProduct {
            get { return isShowCreateProduct; }
            set
            {
                isShowCreateProduct = value;
                CreateProductVisibility = value ? "Visible" : "Collapsed";
                OnPropertyChanged();
            }
        }

        private string createProductVisibility;
        public string CreateProductVisibility
        {
            get { return createProductVisibility; }
            set {
                createProductVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string Title { get; set; }

        private ObservableCollection<ProductViewModel> products;
        public ObservableCollection<ProductViewModel> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<ProductViewModel> GetProducts() {
            return ShoppingListModel.GetProducts().Select(x => new ProductViewModel(x));
        }
        
        public abstract void CreateProduct(OrderedProduct orderedProduct);
        public abstract void UpdateProduct(OrderedProduct orderedProduct);
        public abstract void DeleteProduct(int productId);

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
