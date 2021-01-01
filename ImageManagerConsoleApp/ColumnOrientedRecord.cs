using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ColumnOrientedRecord
    {

        private static readonly List<(string Header, int Order)> _Headers;
        private static readonly List<(PropertyInfo Property, string Format, int Order)> _Formats;

        static ColumnOrientedRecord()
        {
            Type infoType = typeof(ImageFileInfo);
            var items = infoType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => new { Prop = p, Attribute = p.GetCustomAttributes(typeof(ColumnOutputAttribute)).OfType<ColumnOutputAttribute>().FirstOrDefault() })
                .Where(a => a.Attribute != null)
                .ToList();

            _Headers = items
                .OrderBy(i => i.Attribute.Order)
                .Select(v => (v.Attribute.ColumnHeader, v.Attribute.Order))
                .ToList();

            _Formats = items
                .OrderBy(i => i.Attribute.Order)
                .Select(v => (v.Prop, v.Attribute.Format, v.Attribute.Order))
                .ToList();
        }


        public static IEnumerable<string> GetPropertyValues(ImageFileInfo info)
        {
            return _Formats
                .Select
                (
                    f =>
                    {
                        var value = f.Property.GetValue(info);
                        if (value == null)
                        {
                            return "";
                        }
                        else
                        {
                            return (f.Format != null) ? string.Format($"{{0:{f.Format}}}", value) : value.ToString();
                        }
                    }
                );
        }

        public static IEnumerable<string> GetPropertyColumnHeaders()
        {
            return _Headers
                .Select(h => h.Header);
        }
    }
}
