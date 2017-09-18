using System;
using Windows.UI.Xaml.Data;

namespace FluentSpotifyApi.Sample.ACF.UWP.Converters
{
    public class NullableBoolToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                if ((bool)value)
                {
                    return System.Net.WebUtility.HtmlDecode("&#xE8FB;");
                }
                else
                {
                    return System.Net.WebUtility.HtmlDecode("&#xE711;");
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
