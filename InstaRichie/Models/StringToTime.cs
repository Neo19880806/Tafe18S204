using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace StartFinance.Models
{
    public class StringToTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            String dtString = (String)value;
            TimeSpan ts = TimeSpan.Parse(dtString);
            return ts;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            TimeSpan ts = (TimeSpan)value;
            String tsString = ts.ToString();
            return tsString;
        }
    }
}
