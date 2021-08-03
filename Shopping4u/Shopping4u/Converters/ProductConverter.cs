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
                return new OrderedProduct()
                {
                    branchProductId = Int32.Parse(values[0].ToString()),
                    unitPrice = Int32.Parse(values[1].ToString()),
                    quantity = Int32.Parse(values[2].ToString()),
                    shoppingListId = Int32.Parse(values[3].ToString()),
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
