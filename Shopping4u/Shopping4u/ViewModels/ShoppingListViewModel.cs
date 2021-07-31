using Shopping4u.BL;
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
        public string Title { get { return GetTitle(); } private set { } }
        public bool readOnly { get { return IsReadOnly(); } private set { } }

        public IEnumerable<ProductViewModel> Products { get { return GetProducts(); } private set { } }


        public abstract string GetTitle(); 
        public abstract bool IsReadOnly();
        public abstract IEnumerable<ProductViewModel> GetProducts();

        public abstract void CreateProduct(ProductViewModel productViewModel);
        public abstract void UpdateProduct(int productId);
        public abstract void DeleteProduct(int productId);
    }
}
