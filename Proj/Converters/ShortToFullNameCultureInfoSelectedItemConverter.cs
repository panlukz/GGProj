using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Proj.Converters
{
    class ShortToFullNameCultureInfoSelectedItemConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new CultureInfo(value.ToString()).NativeName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (CultureInfo c in allCultures)
            {
                if (c.NativeName == value.ToString()) return c;
            }

            return null;

        }
    }
}
