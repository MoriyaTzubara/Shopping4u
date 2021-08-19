using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Shopping4u.BL;
using Shopping4u.Extensions;

namespace Shopping4u.Models
{
    public class ReccomendedShoppingListModel : ShoppingListModel
    {
        public ReccomendedShoppingListModel()
        {
            Products = getProducts();
        }
        public int shoppingListId { get; set; }
        public IEnumerable<OrderedProduct> Products { get; set; }
        private IEnumerable<OrderedProduct> getProducts()
        {
            IBL bl = new BL.BL();
            var products = bl.GetRecommendedList(App.Consumer.id);
            if (products == null)
                return new List<OrderedProduct>();
            return products.Select(x => x.ToOrderedProduct());
        }

        public void CreateProduct(OrderedProduct orderedProduct)
        {
        }
        public void DeleteProduct(int productId)
        {
        }
        public void UpdateProduct(OrderedProduct orderedProduct)
        {
        }
    }
}
