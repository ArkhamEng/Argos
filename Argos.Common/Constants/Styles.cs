using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Common
{
    public struct Styles
    {
        public struct Buttons
        {
            public struct Hidden
            {
                public const string Warning = "btn btn-warning hidden";

                public const string Primary = "btn btn-primary hidden";

                public const string Success = "btn btn-success hidden";

                public const string Danger = "btn btn-danger hidden";
            }

            public struct Disabled
            {
                public const string Warning = "btn btn-warning disabled";

                public const string Primary = "btn btn-primary disabled";

                public const string Success = "btn btn-success disabled";

                public const string Danger = "btn btn-danger disabled";
            }

            public struct XSmall
            {
                public const string Warning = "btn btn-warning btn-xs pull-right";

                public const string Primary = "btn btn-primary btn-xs pull-right";

                public const string Success = "btn btn-success btn-xs pull-right";

                public const string Default = "btn btn-default btn-xs pull-right";

                public const string Danger = "btn btn-danger btn-xs pull-right";

                public const string Info = "btn btn-info btn-xs pull-right";
            }

            public const string Warning = "btn btn-warning";

            public const string Primary = "btn btn-primary";

            public const string Success = "btn btn-success";

            public const string Default = "btn btn-default";

            public const string Danger = "btn btn-danger";

            public const string Info = "btn btn-info";
        }

        public const string Hidden = "hidden";

        public const string Disabled = "disabled";

        public struct Colors
        {
            public const string Success = "success";

            public const string Danger = "danger";

            public const string Warning = "warning";

            public const string Info = "info";
        }

        public struct Icons
        {
            public const string Male = "fa fa-male";

            public const string Female = "fa fa-female";

            public const string Edit = "glyphicon glyphicon-pencil";

            public const string Delete = "glyphicon glyphicon-trash";

            public const string Save = "fa fa-save";

            public const string Activate = "fa fa-recycle";

            public const string Open = "fa fa-folder-open";

            public const string Show = "fa fa-eye";
        }

        public struct ButtonContent
        {
            public const string Detail = "<span class='fa fa-eye'></span> Detalle";

            public const string New = "<span class='fa fa-file-o'></span> Nuevo";

            public const string Eliminate = "<span class='fa fa-trash'></span> Eliminar";

            public const string Add = "<span class='fa fa-plus'></span> Agregar";

            public const string Reload = "<span class='fa fa-refresh'></span> Recargar";

            public const string Edit = "<span class='fa fa-edit'></span> Editar";

            public const string Save = "<span class='fa fa-save'></span> Guardar";

            public const string Cancel = "<span class='fa fa-times'></span> Cancelar";

            public const string Reactivate = "<span class='fa fa-recycle'></span> Reactivar";
        }
    }
}
