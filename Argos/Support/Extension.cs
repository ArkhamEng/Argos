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
using Argos.Models;
using Argos.Models.HumanResources;

namespace Argos.Support
{
    public static class Extens
    {
        #region Date Helpers
        public static DateTime ToLocal(this DateTime serverDate)
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverDate,
                TimeZoneInfo.Local.Id, "Central Standard Time (Mexico)");

            return localTime;
        }

        public static DateTime TodayLocal(this DateTime serverDate)
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverDate,
                TimeZoneInfo.Local.Id, "Central Standard Time (Mexico)");

            var today = new DateTime(localTime.Year, localTime.Month, localTime.Day);

            return today;
        }
        #endregion

        public static Employee ToEmployee(this IIdentity user)
        {
            using (var db = new ApplicationDbContext())
            {
                var id = user.GetUserId();
                var employee =  db.Entities.OfType<Employee>().FirstOrDefault(e => e.SystemUser.UserId == id);
                return employee;
            }
        }

        public static SelectList ToSelectList(this IEnumerable data)
        {
            return new SelectList(data, nameof(ISelectable.Value), nameof(ISelectable.Text));
        }

        public static SelectList ToSelectList(this IEnumerable data,object selectedValue)
        {
            return new SelectList(data, nameof(ISelectable.Value), nameof(ISelectable.Text),selectedValue);
        }


        public static string GetCode(string serviceCode, DateTime beginDate, int sequential)
        {
            var year = DateTime.Now.ToLocal().ToString("yy");
         
            var seq   = sequential.ToString(Cons.SeqFormat);

            var code = string.Format(Cons.AccoutMask, serviceCode, year, seq);

            return code;
        }

         public static int GetBranchId(this IIdentity user)
        {
            return 2;//user.GetBranchSession().Id;
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


        #region Finantial Methods
        public static double RoundMoney(this double amount)
        {
            return Math.Round(amount, Cons.MoneyDecimals);
        }

        public static string ToText(this double value)
        {
            var IntPart = value.toText() + " PESOS";

            var dv = Math.Round((value - Math.Truncate(value)) * Cons.OneHundred, Cons.Two);

            var decPart = " CON " + dv.toText() + " CENTAVOS";

            return IntPart + decPart;
        }

        private static string toText(this double value)
        {
            string Num2Text = "";

            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }

        #endregion
    }
}