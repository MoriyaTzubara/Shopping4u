using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models.Charts;

namespace Shopping4u.ViewModels.Charts
{
    public class ProductsGroupViewModel
    {
        ProductsGroupModel productsGroupModel;
        public Dictionary<double, Dictionary<string, string>> ProductsGroup { get; set; }

        public ProductsGroupViewModel()
        {
            productsGroupModel = new ProductsGroupModel();
            ProductsGroup = productsGroupModel.getProductsGroup();
        }
         
    }
}
