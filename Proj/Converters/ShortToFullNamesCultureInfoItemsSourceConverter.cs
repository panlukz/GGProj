using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Proj.Converters
{
    class ShortToFullNamesCultureInfoItemsSourceConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            List<string> langs = new List<string>();

            foreach (var item in value as IEnumerable<CultureInfo>)
            {
                langs.Add(item.NativeName);
            }

            return langs;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {           
            return value;
        }
    }
}
