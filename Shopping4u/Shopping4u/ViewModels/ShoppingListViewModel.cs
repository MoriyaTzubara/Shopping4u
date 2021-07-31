using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class ShoppingListViewModel
    {
        //IBL bl = new BL.BL();

        public String Title { get; set; }
        public bool readOnly { get; set; }
        public List<ProductViewModel> Products { get; set; }

        public void DeleteProduct(int productId)
        {
            Products.RemoveAll(p => p.Product.branchProductId == productId);
        }

        public void CreateProduct(ProductViewModel productViewModel)
        {
            Products.Add(productViewModel);
        }

        public void UpdateProduct(int productId)
        {
            
        }
    }
}
