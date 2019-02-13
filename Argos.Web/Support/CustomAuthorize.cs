using Argos.Common;
using Argos.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Argos.Support
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorize:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.StatusCode = Responses.Codes.UnAuthorized;
                context.Result = new JsonResult
                {
                    Data = new 
                    {
                        Header = "Acceso denegado!!",
                        Body   = "Tu sesión ha expirado o te faltan permisos",
                        Result = Responses.Warning,
                        Code   = Responses.Codes.UnAuthorized,
                        Extra  =  URis.Login
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                // this is a standard request, let parent filter to handle it
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}