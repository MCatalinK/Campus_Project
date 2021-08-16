using Campus_APP.ViewModels;
using System;
using System.Windows.Data;

namespace Campus_APP.Converters
{
    class UpdateStudentConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is null
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString())
                || String.IsNullOrEmpty(values[3].ToString())
                || String.IsNullOrEmpty(values[4].ToString())
                || String.IsNullOrEmpty(values[5].ToString()))
            {
                return null;
            }
            else
            {
                var student = values[0] as StudentVM;
                return new StudentVM()
                {
                    Id=(int)student.Id,
                    FirstName = values[1].ToString(),
                    LastName=values[2].ToString(),
                    IdType=Int32.Parse(values[3].ToString()),
                    IdCampus = Int32.Parse(values[4].ToString()),
                    IdRoom = Int32.Parse(values[5].ToString())
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
