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

namespace Shopping4u.ViewModels
{
    public abstract class ShoppingListViewModel: INotifyPropertyChanged
    {
        public ShowCreateProductCommand ShowCreateProductCommand { get; set; }

        public CreateProductCommand CreateProductCommand { get; set; }
        public UpdateProductCommand UpdateProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }

        public ProductConverter ProductConverter { get; set; }



        public ShoppingListViewModel()
        {
            CreateProductCommand = new CreateProductCommand(this);
            UpdateProductCommand = new UpdateProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);

            ShowCreateProductCommand = new ShowCreateProductCommand(this);

            ProductConverter = new ProductConverter();
            IsShowCreateProduct = false;
        }

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

        public abstract void ShowCreateProduct(bool isShow);

        public string Title { get { return GetTitle(); } private set { } }
        public IEnumerable<ProductViewModel> Products { get { return GetProducts(); } private set { } }


        public abstract string GetTitle(); 
        public abstract IEnumerable<ProductViewModel> GetProducts();
        
        public abstract void CreateProduct(OrderedProduct orderedProduct);
        public abstract void UpdateProduct(OrderedProduct orderedProduct);
        public abstract void DeleteProduct(int productId);

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
