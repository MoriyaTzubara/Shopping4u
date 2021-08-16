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
        public OrderedProduct CreateProduct(string imgUrl)
        {
            IBL bl = new BL.BL();
            string productText = bl.EncodeBarcode(imgUrl);
            OrderedProduct product = bl.InsertOrderedProduct(productText, shoppingListId);
            return product;
        }
        public void CreateProduct(OrderedProduct orderedProduct)
        {
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
