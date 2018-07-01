using System.ComponentModel.DataAnnotations;

namespace Argos.Support
{
    public class Cons
    {
        #region Response Status
        public const string ResponseSuccess = "success";

        public const string ResponseWarning = "warning";

        public const string ResponseDanger = "danger";

        public const string ResponseInfo = "info";

        public const string DefaultPassword = "default";

        #endregion

        public const string LoginUrl = "/Account/Login";

        #region File Paths
        public const string ProfileImagePath = "/Files/ProfileImages";

        public const string ProductImagePath = "/Files/ProductImages";

        public const string PersonImagePath = "/Files/PersonImages";

        #endregion

        #region Formats

        public const string AccoutMask = "{0}{1}-{2}";

        public const string SeqFormat = "00000";

        #endregion

        #region Numbers
        public const int Zero = 0;

        public const int One = 1;

        public const int Two = 2;

        public const int OneHundred = 100;

        #endregion



        public const int ImagesPerProduct = 3;


        public const int MoneyDecimals = 4; //se usa para el redondedo de dinero

        public const int AutoCompleateRows = 20;

        public const int MaxSearchRows = 1000;

        public const int LockDuration = 10;


        public const string NoImage = "../Images/sinimagen.jpg";

        public const string NotApply = "No Aplica";

        //gender values
        public const string MaleChar = "M";

        public const string MaleLabel = "Masculino";


        public const string FemaleChar = "F";

        public const string FemaleLabel = "Femenino";



        public const string NoUser = "Sin usuario asignado";


        //product type values
        public const string Package = "Paquete";

        public const string Single = "Individual";


        #region Variables y Sesiones
        public const string BranchSession = "BranchSession";

        public const string IVA = "IVA";
        #endregion
    }

    /// <summary>
    /// Estilos para aplicar en controles html
    /// en base a la evaluación del modelo
    /// </summary>
    public class Styles
    {
        public const string btnWarning          = "btn btn-warning";

        public const string btnWarningHidden    = "btn btn-warning hidden";

        public const string btnWarningDisable   = "btn btn-warning disabled";

        public const string btnPrimary          = "btn btn-primary";

        public const string btnPrimaryHidden    = "btn btn-primary hidden";

        public const string btnPrimaryDisable   = "btn btn-primary disabled";



        public const string btnSuccess = "btn btn-success";

        public const string btnSuccessHidden = "btn btn-success hidden";

        public const string btnSuccessDisable = "btn btn-success disabled";

        public const string btnDanger = "btn btn-danger";

        public const string btnDangerHidden = "btn btn-danger hidden";

        public const string btnDangerDisable = "btn btn-danger disabled";

        public const string Hidden = "hidden";
    }

    public class Icons
    {

        public const string Male = "fa fa-male";

        public const string Female = "fa fa-female";

        public const string Edit = "glyphicon glyphicon-pencil";

        public const string Delete = "glyphicon glyphicon-trash";

        public const string Save = "fa fa-save";

        public const string Activate = "fa fa-recycle";
    }

    public class Codes
    {
        public const int RecordNotFound = -2;
        public const int RecordLocked = -1;
        public const int Success = 200;
        public const int ServerError = 500;
        public const int UnAuthorized = 401;
    }
}