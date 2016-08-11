using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Converters
{
    public class PokeUrlImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value.ToString();
            name = name.ToLowerInvariant()
                .Replace(". ", "-").Replace("♀", "-f").Replace("♂", "-m")
                .Replace("'", string.Empty);
            return $"https://img.pokemondb.net/artwork/{name}.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
