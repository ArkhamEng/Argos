using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Support
{
    public static class Common
    {
        public const string ResponseSuccess = "success";

        public const string ResponseWarning = "warning";

        public const string ResponseDanger  = "danger";

        public const string DefaultPassword = "default";

        public const string UserPicturePath = "/Files/ProfilePictures";

        public const string CodeNumeric = "-0000-0000-0000";

        public const int Zero = 0;

        public const int One = 1;

        

        public static string GetCode(string serviceShorName, int numericValue)
        {
            var code = serviceShorName + numericValue.ToString(CodeNumeric);

            return code;
        }
    }
}