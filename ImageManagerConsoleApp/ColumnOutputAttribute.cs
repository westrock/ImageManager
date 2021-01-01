using System;

namespace ConsoleApp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnOutputAttribute : Attribute
    {
        public string ColumnHeader { get; }

        public int Order { get; }

        public string Format { get; }

        public ColumnOutputAttribute(string columnHeader, int order = 0, string stringFormat = null)
        {
            ColumnHeader = columnHeader;
            Order = order;
            Format = stringFormat;
        }
    }
}
