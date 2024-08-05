using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        static CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");

        static StringExtensions()
        {
            jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
            jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string TrimStartSafe(this string value, params char[] trimChars)
        {
            if (value == null)
                return null;


            return value.TrimStart(trimChars);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string ReformatPhone(this string phone)
        {
            if (phone.IsNullOrEmpty() || !phone.IsValidPhone())
                return phone;

            var digits = Regex.Match(phone, @"\d+").Value;

            if (digits.Length == 9)
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 2), digits.Substring(2, 3), digits.Substring(5, 4));
            else if (digits.Length == 10)
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 3), digits.Substring(3, 3), digits.Substring(6, 4));
            else if (digits.Length == 11)
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 4), digits.Substring(4, 3), digits.Substring(7, 4));
            else if (digits.Length == 12)
                return string.Format("{0}-{1}-{2}-{3}", digits.Substring(0, 3), digits.Substring(3, 2), digits.Substring(5, 3), digits.Substring(8, 4));

            return phone;
        }

        public static string ChangeFormatPhone(this string phone)
        {
            if (phone.IsNullOrEmpty())
                return phone;
            //                                                       +
            return phone.Replace("-", "").Replace(" ", "").Replace("+", "").Replace("&#43;", "").Trim();
        }

        private static readonly string[] phonePrefix9 = { "09", "08", "04", "03", "02" };
        private static readonly string[] phonePrefix10 = { "05", "07" };

        public static bool IsValidPhone(this string phone)
        {
            if (phone == null || phone == string.Empty)
                return false;
        
            phone = phone.Trim();
           
            if (!phone.StartsWith('0'))
            {
                phone = '0' + phone;
            }

            var sb = new StringBuilder(phone.Trim());

            sb.Replace("+972", "0").Replace("&#43;", "+").Replace("+", "0").Replace("-", "").Replace(" ", "");
            if (sb.Length == 9)
            {
                return phonePrefix9.Any(x => phone.StartsWith(x));
            }
            else if (sb.Length == 10)
            {
                return phonePrefix10.Any(x => phone.StartsWith(x));
            }

            return false;
        }

        public static int? ToSafeInt(this string value)
        {
            if (value.IsNullOrEmpty())
                return null;

            int res;
            if (int.TryParse(value, out res))
                return res;

            return null;
        }

        public static int? JustNumber(this string value)
        {
            var digits = value.Where(Char.IsDigit).ToArray();

            string s = new string(digits);

            if (int.TryParse(s, out int result))
                return result;

            return null;
        }

        public static int ToSafeInt(this string value, int defaultValue = 0)
        {
            if (value.IsNullOrEmpty())
                return defaultValue;

            int res;
            if (int.TryParse(value, out res))
                return res;

            return defaultValue;
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static string ToPhone(this string value)
        {
            switch (value.Length)
            {
                case 10:
                    return value.Insert(3, "-");
                case 9:
                    return value.Insert(2, "-");
                default:
                    return value;
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static bool ToBoolean(this string value, bool defValue = false)
        {
            if (value.IsNullOrEmpty())
                return defValue;

            if (value.ToLower() == "true" || value == "1")
                return true;

            if (value.ToLower() == "false" || value == "0")
                return false;

            return defValue;
        }

        public static string MakeReverse(this string s)
        {
            return string.Join(string.Empty, s.Reverse());
        }

        public static DateTime? GetDateFromHebrewString(this string val, int dayInMonth = 1, bool replaceDay = false)
        {
            val = val.Replace("אדר א", "אדר");
            var vals = val.Trim().Split(' ');
            var dayStr = GetDayHebStringFromNumber(dayInMonth);
            var year = vals.Last();

            if (!year.Contains('\"'))
            {
                vals[vals.Length - 1] = year.Insert(year.Length - 1, "\"");
            }

            if (year.StartsWith("ה"))
            {
                vals[vals.Length - 1] = vals[vals.Length - 1].Substring(1);
            }

            if (vals.Length == 2 || (vals.Length == 3 && vals[0] == "אדר"))
            {
                vals[0] = vals[0].Insert(0, $"{dayStr} ");
            }
            else if (vals.Length == 3 && replaceDay)
            {
                vals[0] = dayStr;
            }

            val = string.Join(" ", vals);

            if (DateTime.TryParse(val, jewishCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            return null;
        }

        private static string GetDayHebStringFromNumber(int day)
        {
            switch (day)
            {
                case 1:
                    return "א\'";
                case 2:
                    return "ב\'";
                case 3:
                    return "ג\'";
                case 4:
                    return "ד\'";
                case 5:
                    return "ה\'";
                case 6:
                    return "ו\'";
                case 7:
                    return "ז\'";
                case 8:
                    return "ח\'";
                case 9:
                    return "ט\'";
                case 10:
                    return "י\'";
                case 11:
                    return "י\"א";
                case 12:
                    return "י\"ב";
                case 13:
                    return "י\"ג";
                case 14:
                    return "י\"ד";
                case 15:
                    return "ט\"ו";
                case 16:
                    return "ט\"ז";
                case 17:
                    return "י\"ז";
                case 18:
                    return "י\"ח";
                case 19:
                    return "י\"ט";
                case 20:
                    return "כ\'";
                case 21:
                    return "כ\"א";
                case 22:
                    return "כ\"ב";
                case 23:
                    return "כ\"ג";
                case 24:
                    return "כ\"ד";
                case 25:
                    return "כ\"ה";
                case 26:
                    return "כ\"ו";
                case 27:
                    return "כ\"ז";
                case 28:
                    return "כ\"ח";
                case 29:
                    return "כ\"ט";
                case 30:
                    return "ל\'";
            }

            return "";
        }

        public static string SubStr(this string value, int startIndex, int length)
        {
            if (value == null)
                return null;

            if (value.Length < length)
            {
                length = value.Length;
            }

            return value.Substring(startIndex, length);

        }

        public static string CheckIfStringStartWith(this string str)
        {
            if (str == null)
                return str;

            var chars = new string[] { "@", "-", "+", "=" };
            if (chars.Any(x => str.StartsWith(x)))
            {
                str = '"' + str + '"';
            }
            return str;
        }
    }
}
