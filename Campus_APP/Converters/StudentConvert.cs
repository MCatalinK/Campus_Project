using Campus_APP.ViewModels;
using System;
using System.Windows.Data;

namespace Campus_APP.Converters
{
    class StudentConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString())
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString())
                || String.IsNullOrEmpty(values[3].ToString())
                || String.IsNullOrEmpty(values[4].ToString())
                || values[5] is null
                || String.IsNullOrEmpty(values[5].ToString())
                || values[6] is null
                || String.IsNullOrEmpty(values[6].ToString()))
            {
                return null;
            }
            else
            {
                return new StudentVM()
                {
                    FirstName = values[0].ToString(),
                    LastName=values[1].ToString(),
                    SSN=values[2].ToString(),
                    IdType=Int32.Parse(values[3].ToString()),
                    IdUni = Int32.Parse(values[4].ToString()),
                    IdCampus = Int32.Parse(values[5].ToString()),
                    IdRoom = Int32.Parse(values[6].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            StudentVM student = value as StudentVM;
            object[] result = new object[7] { student.FirstName,student.LastName,student.SSN,student.IdType,student.IdUni,student.IdCampus,student.IdRoom };
            return result;
        }
    }
}
