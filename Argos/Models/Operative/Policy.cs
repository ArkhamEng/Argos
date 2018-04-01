using Argos.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Operative
{
    [Table("Policy", Schema = "Operative")]
    public class Policy : AuditableEntity
    {
        [Column(Order =0),Key,ForeignKey("Account")]
        public int PolicyId { get; set; }

        [Column(Order = 1),ForeignKey("Status")]
        public int StatusId { get; set; }


        [Column(Order = 2)]
        public int CutOffDay { get; set; }

        [Column(Order = 3)]
        [Display(Name = "Próximo corte")]
        [DataType(DataType.DateTime)]
        public DateTime NextCutOff { get; set; }

        [Column(Order = 4)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Column(Order = 5)]
        [Required]
        [Display(Name = "Periodo pago")]
        public int PayFreq { get; set; }

        [Column(Order = 6)]
        [Required]
        [Display(Name = "Periodo Mantto")]
        public int MaintFreq { get; set; }

        #region Navigation Properties
        public virtual Account Account { get; set; }

        public virtual OperativeStatus Status { get; set; }

        public ICollection<PolicyHistory> Histories { get; set; }

        public ICollection<PolicyCharge> Charges { get; set; }
        #endregion
    }

}