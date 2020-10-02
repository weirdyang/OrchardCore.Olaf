using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OrchardCore.Olaf.Common
{
    public static class DateTimeUtils
    {
        public static string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";

        public static DateTimeOffset ParseDateString(string dateExpression)
        {
            DateTimeOffset target;
            bool check = DateTimeOffset.TryParseExact(
                dateExpression,
                DateTimeFormat,
                null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AllowWhiteSpaces,
                out target);

            if (!check)
            {
                target = DateTimeOffset.Parse(dateExpression);
            }

            return target;
        }
    }
}
