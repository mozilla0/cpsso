using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Framework.Helper
{
    public static class ValueTypeHelper
    {
        public static int ToInt(this string val)
        {
            int temp;
            return val == null ? 0 : int.TryParse(val, out temp) ? Convert.ToInt32(val) : 0;
        }

        public static int ToInt(this double val)
        {
            return Convert.ToInt32(val);
        }
        public static int ToInt(this int ? val)
        {
            return Convert.ToInt32(val);
        }

        public static double ToDouble(this string val)
        {
            double temp;
            return val == null ? 0.0d : double.TryParse(val, out temp) ? Convert.ToDouble(val) : 0.0d;
        }

        public static double ToDouble(this decimal val)
        {
            return Convert.ToDouble(val);
        }

        public static decimal ToDecimal(this decimal? val)
        {
            return Convert.ToDecimal(val);
        }

        public static decimal ToDecimal(this double val)
        {
            return Convert.ToDecimal(val);
        }

        public static decimal ToDecimal(this string val)
        {
            decimal temp;
            return val == null ? 0 : decimal.TryParse(val, out temp) ? Convert.ToDecimal(val) : 0;
        }
        public static Boolean ToBoolean(this string val)
        {
            val = val ?? "";
            return val.ToInt() > 0 || val.ToLower() == "true" ? true : false;
        }
        public static Boolean ToBoolean(this int val)
        {
            return Convert.ToBoolean(val);
        }
        public static Boolean ToBoolean(this int ? val)
        {
            return Convert.ToBoolean(val);
        }
        public static Boolean ToBoolean(this bool? val)
        {
            return Convert.ToBoolean(val);
        }
        public static string ToStringValue(this string val)
        {
            return val ?? "";
        }
        public static string ToStringValue(this object val)
        {
            return (val ?? val).ToString();
        }
        public static string ToStringValue(this int ? val)
        {
            int temp;

            string valT  = Convert.ToString(val==null ? "0" : val.ToString());

            return Convert.ToString(valT == null ? 0 : int.TryParse(valT, out temp) ? Convert.ToInt32(valT) : 0);
        }

        public static Guid ToGuid(this string val)
        {
            Guid temp;
            return string.IsNullOrEmpty(val) ? new Guid() : Guid.TryParse(val, out temp) ? Guid.Parse(val) : new Guid();
        }

        public static string ToDateFormatted(this DateTime? date)
        {
            return string.Format("{0:dddd-MMM-yyyy H:mm:ss zzz}", date);
        }

        public static DateTime ToDateTime(this DateTime? date)
        {
            return Convert.ToDateTime(date);
        }

        public static DateTime ToDate(this string dt)
        {
            var NewDate = new DateTime { };
            string[] DateArray = dt.Split('-');
            if (DateArray.Length > 2)
            {
                NewDate = Convert.ToDateTime(DateArray[0] + "-" + DateArray[1] + "-" + DateArray[2]);
            }
            return NewDate;
        }

        public static byte ToByte(this byte? val)
        {
            return Convert.ToByte(val);
        }

        public static byte ToByte(this int val)
        {
            return Convert.ToByte(val);
        }

        public static string ToMonthName(this int val)
        {
            string[] months = {"","Jan","Feb", "Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec" };
            return val > 0 && val <= 12 ? months[val] : months[0];
        }
        
    }
}
