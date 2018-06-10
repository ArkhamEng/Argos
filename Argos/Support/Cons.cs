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

        #region Formats

        public const string AccoutMask ="{0}{1}-{2}";

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

        public const int MaxSearchRows = 400;

        public const int LockDuration = 10;


        public const string NoImage = "../Images/sinimagen.jpg";

        public const string NotApply = "No Aplica";

        //gender values
        public const string MaleChar  = "M";

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
        public const string BtnEdit             = "btn btn-warning";

        public const string BtnEditDisabled     = "btn btn-warning disabled";

        public const string BtnEditHidden       = "btn btn-warning hidden";

        public const string BtnDelete           = "btn btn-danger";

        public const string BtnDeletetDisabled  = "btn btn-danger disabled";

        public const string BtnDeletetHidden    = "btn btn-danger hidden";

        public const string BtnActivate         = "btn btn-success";

        public const string BtnActivateDisabled = "btn btn-danger disabled";

        public const string BtnActivateHidden   = "btn btn-danger hidden";       
    }

    public class Icons
    {

        public const string Male     = "fa fa-male";

        public const string Female   = "fa fa-female";

        public const string Edit     = "glyphicon glyphicon-pencil";

        public const string Delete   = "glyphicon glyphicon-trash";

        public const string Save     = "fa fa-save";

        public const string Activate = "fa fa-recycle";
    }
}