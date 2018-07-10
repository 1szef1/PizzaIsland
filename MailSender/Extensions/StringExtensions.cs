using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Extensions
{
    internal static class StringExtensions
    {
        public static string Encode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(text.Reverse().ToArray());
            return "pi" + Convert.ToBase64String(bytes);
        }

        public static string Decode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var bytes = Convert.FromBase64String(text.Substring(2, text.Length - 2));
            return Encoding.UTF8.GetString(bytes.Reverse().ToArray());
        }
    }
}
