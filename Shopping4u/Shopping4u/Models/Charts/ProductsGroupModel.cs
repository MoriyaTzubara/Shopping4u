using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class ProductsGroupModel
    {
        public Dictionary<double, Dictionary<string, string>> getProductsGroup()
        {
            IBL bl = new BL.BL();
            return bl.ProductsBoughtTogether(App.Consumer.id);
        }

    }
}
