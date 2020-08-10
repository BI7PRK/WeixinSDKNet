using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.Extensions
{
    public static class DateTimeConver
    {
        static DateTime minDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        public static long ToMillisecond(this DateTime time)
        {
            if (time == DateTime.MinValue)
                return 0L;
            double value = (time - minDate).TotalMilliseconds;
            return (long)Math.Floor(value);  
        }

        public static DateTime FromMillSeconds(this long msds)
        {
            return minDate.AddMilliseconds(msds);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToUnixSeconds(this DateTime time)
        {
            if (time == DateTime.MinValue)
                return 0;
            double value = time.Subtract(minDate).TotalSeconds;
            return (int)Math.Floor(value);
        }


        public static DateTime FromUnixSeconds(this int seconds)
        {
            return minDate.AddSeconds(seconds);
        }

       
    }
}
