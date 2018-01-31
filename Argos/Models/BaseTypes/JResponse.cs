using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public class JResponse
    {
        public int Id { get; set; }

        public string Result { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }

        public string Extra { get; set; }

        public const string Success = "success";
        public const string Warning = "warning";
        public const string Confirm = "confirm";
    }
}