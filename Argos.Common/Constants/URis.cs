using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Common
{
    public struct URis
    {
        public struct Images
        {
            public const string Profile = "/Files/Images/Profile";

            public const string Product = "/Files/Images/Product";

            public const string Employee = "/Files/Images/Employee";

            public const string Supplier = "/Files/Images/Supplier";

            public const string Customer = "/Files/Images/Customer";

            public const string TradeMark = "/Files/Images/TradeMark";
        }

        public struct Files
        {
            public const string OperativeReport = "/Files/Reports/Operative";

            public const string CreditNote      = "/Files/Documents/CreditNote";

            public const string Bill            = "/Files/Documents/Bill";

            public const string Refundment      = "/Files/Documents/Refundment";
        }

        public const string Login     = "/Account/Login";

        public const string NoImage = "../Images/sinimagen.jpg";

    }
}
