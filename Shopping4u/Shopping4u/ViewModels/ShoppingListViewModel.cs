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
using System.Windows;

namespace Shopping4u.ViewModels
{
    public abstract class ShoppingListViewModel : INotifyPropertyChanged
    {

        public ShoppingListModel ShoppingListModel;

        public ShoppingListViewModel(ShoppingListModel shoppingListModel)
        {
            ShoppingListModel = shoppingListModel;

            Products = new ObservableCollection<OrderedProductViewModel>(shoppingListModel.Products.Select(x => new OrderedProductViewModel(x)));
            
            CreateProductCommand = new CreateProductCommand(this);
            UpdateProductCommand = new UpdateProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);
            ExportRecommendedListToPDFCommand = new ExportRecommendedListToPDFCommand(this);

            ShowCreateProductCommand = new ShowCreateProductCommand(this);

            ProductConverter = new ProductConverter();
            IsShowCreateProduct = false;

            numberOfProducts = Products.Count;
            TotalPrice = calculateTotalPrice();
        }


        internal void ExportRecommendedListToPDF()
        {
            MessageBox.Show("Export to PDF");
        }

        public ShowCreateProductCommand ShowCreateProductCommand { get; set; }
        public CreateProductCommand CreateProductCommand { get; set; }
        public UpdateProductCommand UpdateProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }
        public ExportRecommendedListToPDFCommand ExportRecommendedListToPDFCommand { get; set; }


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

        private ObservableCollection<OrderedProductViewModel> products;
        public ObservableCollection<OrderedProductViewModel> Products
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

        public IEnumerable<OrderedProductViewModel> GetProducts() {
            return ShoppingListModel.Products.Select(x => new OrderedProductViewModel(x));
        }
        
        public virtual void CreateProduct(OrderedProduct orderedProduct)
        {
            products.Add(new OrderedProductViewModel(orderedProduct));

            NumberOfProducts += 1;
            TotalPrice += orderedProduct.unitPrice * orderedProduct.quantity;
        }
        public virtual void DeleteProduct(int productId)
        {
            var product = Products.First(x => x.Id == productId);
            Products.Remove(product);

            NumberOfProducts -= 1;
            TotalPrice -= product.orderedProduct.unitPrice * product.Quantity;
        }
        public virtual void UpdateProduct(OrderedProduct orderedProduct)
        {
            int index = Products.IndexOf(Products.First(x => x.Id == orderedProduct.id));
            Products[index] = new OrderedProductViewModel(orderedProduct);

            TotalPrice = calculateTotalPrice();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int numberOfProducts;
        public int NumberOfProducts
        {
            get { return numberOfProducts; }
            set 
            {
                numberOfProducts = value;
                OnPropertyChanged();
            }
        }


        private double totalPrice { get; set; }
        public double  TotalPrice 
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }
        private double calculateTotalPrice()
        {
            return Products.Sum(x => x.orderedProduct.unitPrice * x.Quantity);
        }

    }
}
