using Core.Utils;

namespace DusiApp.Converters;
public class EnumToStringConverter<T> : IValueConverter where T : Enum
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (int)value;
        var list = EnumHelper.ToList(typeof(T));
        var res = list.Where(e => e.Value == val)
            .Select(e => e.Description)
            .FirstOrDefault();
        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (string)value;
        var list = EnumHelper.ToList(typeof(T));
        var res = list.Where(e => e.Description == val)
            .Select(e => e.Value)
            .FirstOrDefault();
        return res;
    }
}
