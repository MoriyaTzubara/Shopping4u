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
        public IEnumerable<OrderedProduct> Products { get; set; }
        public IEnumerable<BranchProduct> BranchProducts { get; set; }

        public ReccomendedShoppingListModel()
        {
            Products = GetProducts();

        }

        public IEnumerable<OrderedProduct> GetProducts()
        {
            IBL bl = new BL.BL();
            return bl.GetRecommendedList(123).Select(x => x.ToOrderedProduct());
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
