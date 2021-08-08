using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public interface ShoppingListModel
    {
        IEnumerable<OrderedProduct> Products { get; set; }
        IEnumerable<BranchProduct> BranchProducts { get; set; }

        IEnumerable<OrderedProduct> GetProducts();

        void CreateProduct(OrderedProduct orderedProduct);
        void UpdateProduct(OrderedProduct orderedProduct);
        void DeleteProduct(int productId);
    }
}
