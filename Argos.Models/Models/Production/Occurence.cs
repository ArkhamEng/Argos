using Argos.Models.BaseTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Production
{
    [Table("Occurence", Schema = "Production")]
    public abstract class Occurence : ActivableAudit
    {
        public int OccurenceId { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [Display(Name ="Fecha Programada")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Fecha Límite")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }


        [MaxLength(100)]
        [Display(Name = "Observaciones")]
        public string Comment { get; set; }

        [Display(Name = "Completado")]
        public bool IsDone { get; set; }


        public virtual Account Account { get; set; }
    }
}
