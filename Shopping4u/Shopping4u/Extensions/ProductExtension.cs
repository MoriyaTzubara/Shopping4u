using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Shopping4u.BL;

namespace Shopping4u.Extensions
{
    public static class ProductExtension
    {
        public static OrderedProduct ToOrderedProduct(this Product product)
        {
            IBL bl = new BL.BL();
            return bl.ConvertProductToOrderedProduct(product);
        }
    }
}
