using Argos.Models.BaseTypes;
using Argos.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Production
{
    [Table("MaintPeriod", Schema = "Production")]
    public class MaintPeriod: ActivableAudit, ISelectable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaintPeriodId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }



        public string Value
        {
            get
            {
                return this.MaintPeriodId.ToString();
            }
        }

        public string Text
        {
            get
            {
                return this.Name;
            }
        }

        public ICollection<Account> Accounts { get; set; }
    }
}

