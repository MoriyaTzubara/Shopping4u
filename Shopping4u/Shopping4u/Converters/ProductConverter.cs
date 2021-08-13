using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopping4u.Extensions;

namespace Shopping4u.Converters
{
    public class ProductConverter : IMultiValueConverter 
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return new OrderedProduct();
            try
            {
                int branchProductId;
                int quantity;
                double unitPrice;
                int shoppingListId;
                int id;

                return new OrderedProduct()
                {
                    branchProductId = Int32.TryParse(values[0].getOrElse("").ToString(), out branchProductId) ? branchProductId : 0,
                    unitPrice = double.TryParse(values[1].getOrElse("").ToString().Replace("$", ""), out unitPrice) ? unitPrice : 0.0,
                    quantity = Int32.TryParse(values[2].getOrElse("").ToString(), out quantity) ? quantity : 0,
                    shoppingListId = Int32.TryParse(values[3].getOrElse("").ToString(), out shoppingListId) ? shoppingListId : 0,
                    id = Int32.TryParse(values[4].getOrElse("").ToString(), out id) ? id : 0,
                };

            }
            catch
            {
                return new OrderedProduct();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            OrderedProduct orderedProduct = value as OrderedProduct;
            return new object[]
            {
                orderedProduct.branchProductId,
                orderedProduct.unitPrice,
                orderedProduct.quantity 
            };
        }
    }
}
