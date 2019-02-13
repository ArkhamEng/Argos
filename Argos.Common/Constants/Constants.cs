namespace Argos.Common.Constants
{
    public struct Numbers
    {
        public const int Zero = 0;

        public const int One = 1;

        public const int Two = 2;

        public const int OneHundred = 100;

        public const int MilisecPerMinute = 60000;

        public struct Config
        {
            /// <summary>
            /// Cantidad de imagenes que puede tener un producto
            /// </summary>
            public const int ImagesPerProduct = 3;

            /// <summary>
            /// Decimales a manajar con el dinero
            /// </summary>
            public const int MoneyDecimals = 4; //se usa para el redondedo de dinero

            /// <summary>
            /// Cantidad máxima de registros a mostrar en un autocompleate
            /// </summary>
            public const int AutoCompleateRows = 20;

            /// <summary>
            /// Cantidad máxima de registros a buscar
            /// </summary>
            public const int MaxSearchRows = 1000;

            /// <summary>
            /// Duración del bloqueo a un registro en minutos
            /// </summary>
            public const int LockDuration = 10;

            /// <summary>
            /// Tiempo de expiración de la sesión en minutos
            /// </summary>
            public const int SessionExpTime = 15;

            public const int SalePriceDecimals = 2;

            public const int DaysToPay = 15;

        }
    }



    public struct Responses
    {
        public const string Success = "success";

        public const string Warning = "warning";

        public const string Danger = "danger";

        public const string Info = "info";

        public struct Codes
        {
            public const int RecordNotFound = -2;
            public const int RecordLocked   = -1;
            public const int Success        = 200;
            public const int ServerError    = 500;
            public const int UnAuthorized   = 401;
        }
    }

    public struct Formats
    {
        public const string AccoutMask = "{0}-{1}";

        public const string Sequential = "00000";
    }

 

    public struct Labels
    {
        public const string Package = "Paquete";

        public const string Single = "Individual";

        public const string MaleChar = "M";

        public const string FemaleChar = "F";

        public const string Male = "Masculino";

        public const string Female = "Femenino";

        public const string NoUser = "Sin usuario asignado";

        public const string NotApply = "No Aplica";

        public const string IVA = "IVA";

        public struct Config
        {
            public const string DefaultPassword = "123456";

            public const string BranchSession = "BranchSession";
        }

    }

}
