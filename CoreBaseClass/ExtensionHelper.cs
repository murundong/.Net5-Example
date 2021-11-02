using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreBaseClass
{
    public static class ExtensionHelper
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return (
               method.ReturnType == typeof(Task) ||
               (method.ReturnType.GetTypeInfo().IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
           );
        }
        public static DateTime ToDateTime(this long stamp)
        {
            return TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local).AddSeconds(stamp);

            //return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(stamp);
        }

        public static DateTime GetFirstWeekDay(this DateTime dt)
        {
            int weeknow = (int)dt.DayOfWeek;
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            return Convert.ToDateTime(dt.AddDays(daydiff).ToString("yyyy-MM-dd"));
        }

        public static DateTime NowDate(this DateTime dt)
        {
            return Convert.ToDateTime(dt.ToString("yyyy-MM-dd"));
        }

        public static DateTime GetLastDayOfWeek(this DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            return Convert.ToDateTime(dt.AddDays(daydiff).ToString("yyyy-MM-dd"));
        }

        public static DateTime GetPrevFirstWeekDay(this DateTime dt)
        {
            return Convert.ToDateTime(DateTime.Now.AddDays(0 - Convert.ToInt16(DateTime.Now.DayOfWeek) - 7 + 1).ToString("yyyy-MM-dd"));
        }

        public static DateTime GetPrevLastWeekDay(this DateTime dt)
        {
            return Convert.ToDateTime(DateTime.Now.AddDays(6 - Convert.ToInt16(DateTime.Now.DayOfWeek) - 7 + 1).ToString("yyyy-MM-dd"));
        }

        public static long GetTimeTicks(this DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
