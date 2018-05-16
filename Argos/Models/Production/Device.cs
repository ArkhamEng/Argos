using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    public class Device
    {
        public int DeviceId { get; set; }

        public int IMEI { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }


    }
}