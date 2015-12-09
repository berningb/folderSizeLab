using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SeperateFileTest
{
    class filesizeconvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                //prefixes
                string[] sizes = { "B", "KB", "MB", "GB" };
                //filesize in bytes
                long len = (long) value;
                //get which prefix to use
                int order = 0;
                //if the current size can be reduced reduce it and increase the order 
                while (len >= 1024 && order + 1 < sizes.Length)
                {
                    order++;
                    len = len / 1024;
                }

                // create the result string that shows the length and adds the prefix to the end of it
                string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                return result;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
