using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace StartFinance.Models
{
    public class StringToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            String dtString = (String)value;
            DateTime dt = DateTime.Parse(dtString);
            return new DateTimeOffset(dt);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTime dt = DateTime.Now;

            if (value is DateTimeOffset)
            {
                DateTimeOffset offset = (DateTimeOffset)value;
                dt = offset.DateTime;
            }
            else
            {
                dt = (DateTime)value;
            }
            String dtString = dt.ToString();
            return dtString;
        }
    }
}
