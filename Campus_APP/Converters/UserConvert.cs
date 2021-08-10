using Campus_APP.ViewModels;
using System;
using System.Windows.Data;

namespace Campus_APP.Converters
{
    class UserConvert: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                return new UserVM()
                {
                    Username = values[0].ToString(),
                    Password = values[1].ToString(),
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            UserVM user = value as UserVM;
            object[] result = new object[2] {user.Username, user.Password };
            return result;
        }
    }
}
