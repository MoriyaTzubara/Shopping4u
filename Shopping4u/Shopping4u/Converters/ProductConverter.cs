using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Shopping4u.Converters
{
    class ProductConverter : IMultiValueConverter
    {
        private OrderedProduct orderedProduct;

        public ProductConverter(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return orderedProduct;

            return new OrderedProduct(orderedProduct)
            {
                branchProductId = (int) values[0],
                unitPrice = (double) values[1],
                quantity = (int) values[2]                
            };
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
