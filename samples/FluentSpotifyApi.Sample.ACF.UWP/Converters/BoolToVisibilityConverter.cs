using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FluentSpotifyApi.Sample.ACF.UWP.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }

        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return this.TrueValue;
            }
            else
            {
                return this.FalseValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
