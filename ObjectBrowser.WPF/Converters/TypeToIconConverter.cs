using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.WPF.Converters
{
    public class TypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TypeKind)value)
            {
                case TypeKind.EnumType:
                    return new BitmapImage(new Uri("/Assets/enum.jpeg", UriKind.Relative));
                case TypeKind.StructType:
                    return new BitmapImage(new Uri("/Assets/struct.jpeg", UriKind.Relative));
                case TypeKind.InterfaceType:
                    return new BitmapImage(new Uri("/Assets/interface.jpeg", UriKind.Relative));
                case TypeKind.ClassType:
                    return new BitmapImage(new Uri("/Assets/class.jpeg", UriKind.Relative));
                case TypeKind.Namespace:
                    return new BitmapImage(new Uri("/Assets/namespace.jpeg", UriKind.Relative));
                case TypeKind.Assembly:
                    return new BitmapImage(new Uri("/Assets/asm.jpeg",UriKind.Relative));
                case TypeKind.MethodType:
                    return new BitmapImage(new Uri("/Assets/method.jpeg", UriKind.Relative));
                case TypeKind.Property:
                    return new BitmapImage(new Uri("/Assets/prop.jpeg", UriKind.Relative));
                case TypeKind.Field:
                    return new BitmapImage(new Uri("/Assets/field.jpeg", UriKind.Relative));
                case TypeKind.EnumField:
                    return new BitmapImage(new Uri("/Assets/enum.jpeg", UriKind.Relative));
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }    

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
