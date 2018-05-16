using System.ComponentModel.DataAnnotations;

namespace Argos.Support
{
    public class Cons
    {
        #region Response Status
        public const string ResponseSuccess = "success";

        public const string ResponseWarning = "warning";

        public const string ResponseDanger = "danger";

        public const string DefaultPassword = "default";

        #endregion

        #region File Paths
        public const string UserPicturePath = "/Files/ProfilePictures";

        public const string ProductImagePath = "/Files/ProductImages";

        #endregion

        public const string AccoutMask ="{0}{1}-{2}";

        public const string SeqFormat = "00000";

        public const int Zero = 0;

        public const int One = 1;

        public const int Two = 2;

        public const int OneHundred = 100;

        public const int MoneyDecimals = 4; //se usa para el redondedo de dinero

        public const int AutoCompleateRows = 20;

        public const int MaxSearchRows = 400;

        public const int LockDuration = 10;

    //gender values
        public const string Male  = "Masculino";

        public const string Female = "Femenino";


       //product type values
        public const string Package = "Paquete";

        public const string Single = "Individual";


        #region Variables y Sesiones
        public const string BranchSession = "BranchSession";

        public const string IVA = "IVA";
        #endregion
    }
}