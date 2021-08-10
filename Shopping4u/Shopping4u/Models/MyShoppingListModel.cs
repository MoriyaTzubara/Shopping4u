using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Shopping4u.BL;
using Shopping4u.ViewModels;

namespace Shopping4u.Models
{
    public class MyShoppingListModel : ShoppingListModel
    {

        public MyShoppingListModel()
        {
            Products = getProducts();
        }

        public IEnumerable<OrderedProduct> Products { get; set; }
        private IEnumerable<OrderedProduct> getProducts()
        {
            IBL bl = new BL.BL();
            return bl.CreateUnapprovedShoppingList(123).products;
        }

        public void CreateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
        }
        public void UpdateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            bl.UpdateOrderedProduct(orderedProduct);
        }
        public void DeleteProduct(int productId)
        {
        }
    }
}
