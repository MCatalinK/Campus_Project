using Campus_APP.ViewModels;
using System;
using System.Windows.Data;

namespace Campus_APP.Converters
{
    class UpdateStudentConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString())
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString())
                || String.IsNullOrEmpty(values[3].ToString())
                || String.IsNullOrEmpty(values[4].ToString()))
            {
                return null;
            }
            else
            {
                return new StudentVM()
                {
                    FirstName = values[0].ToString(),
                    LastName=values[1].ToString(),
                    IdType=Int32.Parse(values[2].ToString()),
                    IdCampus = Int32.Parse(values[3].ToString()),
                    IdRoom = Int32.Parse(values[4].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            StudentVM student = value as StudentVM;
            object[] result = new object[5] { student.FirstName,student.LastName,student.IdType,student.IdCampus,student.IdRoom };
            return result;
        }
    }
}
