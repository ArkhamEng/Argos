using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Analytic
{
    [Table("ErroLog", Schema = "Analytic")]
    public class ErrorLog
    {
        public int ErrorLogId { get; set; }

        [MaxLength(50)]
        public string Action { get; set; }

        [MaxLength(50)]
        public string Controller { get; set; }


        public string Description { get; set; }


        public DateTime InsDate { get; set; }

        public ErrorLog()
        {
            this.InsDate = DateTime.Now.ToLocal();
        }
    }
}
