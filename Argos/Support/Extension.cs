using Argos.Models.BaseTypes;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

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
            return new SelectList(data, nameof(ISelectable.Value), nameof(ISelectable.Text));
        }


        public static string GetCode(string serviceShorName, int numericValue)
        {
            var code = serviceShorName + numericValue.ToString(Cons.CodeNumeric);

            return code;
        }

         public static int GetBranchId(this IIdentity user)
        {
            return user.GetBranchSession().Id;
        }

        public static JResponse GetBranchSession(this IIdentity user)
        {
            var um = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = user.GetUserId();
            var claim = um.GetClaims(userId).FirstOrDefault(c => c.Type == Cons.BranchSession);

            JResponse session;

            if (claim != null)
            {
                var sArry = claim.Value.Split(',');
                session = new JResponse { Id = Convert.ToInt32(sArry[0]), Extra = sArry[1] };
            }
            else
                session = new JResponse();

            return session;
        }

    }
}