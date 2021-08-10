using Campus_APP.ViewModels;
using System;
using System.Windows.Data;

namespace Campus_APP.Converters
{
    class CampusConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString())
                || values[1] is null
                || String.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }
            else
            {
                return new CampusVM()
                {
                    IdUni = Int32.Parse(values[0].ToString()),
                    Tax = Decimal.Parse(values[1].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            CampusVM campus = value as CampusVM;
            object[] result = new object[2] { campus.IdUni,campus.Tax };
            return result;
        }
    }
}
