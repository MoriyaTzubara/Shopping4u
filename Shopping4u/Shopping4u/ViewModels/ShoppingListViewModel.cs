using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public abstract class ShoppingListViewModel
    {
        public CreateProductCommand CreateProductCommand { get; set; }
        public UpdateProductCommand UpdateProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }

        public ProductConverter ProductConverter { get; set; }


        public ShoppingListViewModel()
        {
            CreateProductCommand = new CreateProductCommand(this);
            UpdateProductCommand = new UpdateProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);

            ProductConverter = new ProductConverter();
        }

        public string Title { get { return GetTitle(); } private set { } }
        public bool readOnly { get { return IsReadOnly(); } private set { } }

        public IEnumerable<ProductViewModel> Products { get { return GetProducts(); } private set { } }


        public abstract string GetTitle(); 
        public abstract bool IsReadOnly();
        public abstract IEnumerable<ProductViewModel> GetProducts();

        public abstract void CreateProduct(OrderedProduct orderedProduct);
        public abstract void UpdateProduct(OrderedProduct orderedProduct);
        public abstract void DeleteProduct(int productId);

    }
}
