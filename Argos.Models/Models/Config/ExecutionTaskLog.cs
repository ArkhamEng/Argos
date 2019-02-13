using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Analytic
{
    [Table("ExecutionTaskLog", Schema = "Analytic")]
    public class ExecutionTaskLog
    {
        public int ExecutionTaskLogId { get; set; }

        [MaxLength(50)]
        public string TaskName { get; set; }

        public string Comment { get; set; }

        public bool HasSucced { get; set; }

        public int Duration { get; set; }


        public DateTime InsDate { get; set; }
    }
}
