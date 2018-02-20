using Argos.Models.BaseTypes;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;

namespace Argos.Support
{
    public static class Extens
    {
        public static DateTime ToLocal(this DateTime serverDate)
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverDate,
                TimeZoneInfo.Local.Id, "Central Standard Time (Mexico)");

            return localTime;
        }

        public static SelectList ToSelectList(this IEnumerable data)
        {
            return new SelectList(data, nameof(ISelectable.Id), nameof(ISelectable.Name));
        }


        public static string GetCode(string serviceShorName, int numericValue)
        {
            var code = serviceShorName + numericValue.ToString(Cons.CodeNumeric);

            return code;
        }

    }
}