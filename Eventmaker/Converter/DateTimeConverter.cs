using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaker.Converter
{
    static class DateTimeConverter
    {
        
        public static DateTime DateTimeOffsetAndDateTime(DateTimeOffset offset, TimeSpan timespan)
        {
            return  new DateTime(offset.Year, offset.Month, offset.Day, timespan.Hours, timespan.Minutes, timespan.Seconds);
        }
    }
}
