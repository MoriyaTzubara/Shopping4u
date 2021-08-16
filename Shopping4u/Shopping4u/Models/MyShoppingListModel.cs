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
        public int shoppingListId { get; set; }
        public MyShoppingListModel()
        {
            Products = getProducts();
        }
        public IEnumerable<OrderedProduct> Products { get; set; }
        private IEnumerable<OrderedProduct> getProducts()
        {
            IBL bl = new BL.BL();
            var shoppingList = bl.CreateUnapprovedShoppingList(123);
            shoppingListId = shoppingList.id;
            return shoppingList.products;
        }

        public void CreateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            orderedProduct.shoppingListId = shoppingListId;
            bl.InsertOrderedProduct(orderedProduct);
        }
        public void UpdateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            bl.UpdateOrderedProduct(orderedProduct);
        }
        public void DeleteProduct(int orderedProductId)
        {
            IBL bl = new BL.BL();
            bl.DeleteOrderedProduct(orderedProductId);
        }

        public void SaveShoppingList()
        {
            IBL bl = new BL.BL();
            bl.SaveShoppingList(shoppingListId);
        }

        public int NewShoppingList()
        {
            IBL bl = new BL.BL();
            ShoppingList shoppingList = bl.CreateUnapprovedShoppingList(123);
            shoppingListId = shoppingList.id;
            Products = new List<OrderedProduct>();
            return shoppingListId;
        }
    }
}
