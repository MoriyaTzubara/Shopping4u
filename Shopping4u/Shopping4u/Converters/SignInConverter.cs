using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Shopping4u.ViewModels;

namespace Shopping4u.Converters
{
    public class SignInConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return new SignInViewModel();

            string password = new NetworkCredential("", (values[1] as PasswordBox).SecurePassword).Password;

            return new SignInViewModel()
            {
                Email = values[0].ToString(),
                Password = password
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new object[] { "", "" };

            return new object[]
            {
                (value as SignInViewModel).Email,
                (value as SignInViewModel).Password,
            };
        }
    }
}
