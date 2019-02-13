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
using Argos.Common.Constants;
using Argos.Data.Context;
using Argos.Web;
using Argos.Models.Interfaces;

namespace Argos.Support
{
    public static class Extens
    {
        public static Employee ToEmployee(this IIdentity user)
        {
            using (var db = new ApplicationDbContext())
            {
                var id = user.GetUserId();
                var employee = db.Entities.OfType<Employee>().FirstOrDefault(e => e.SystemUser.UserId == id);
                return employee;
            }
        }

        public static int GetBranchId(this IIdentity user)
        {
            return 2;//user.GetBranchSession().Id;
        }

        public static JResponse GetBranchSession(this IIdentity user)
        {
            var um = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = user.GetUserId();
            var claim = um.GetClaims(userId).FirstOrDefault(c => c.Type == Labels.Config.BranchSession);

            JResponse session;

            if (claim != null)
            {
                var sArry = claim.Value.Split(',');
                session = new JResponse { Id = Convert.ToInt32(sArry[Numbers.Zero]), Extra = sArry[Numbers.One] };
            }
            else
                session = new JResponse();

            return session;
        }

    }
}