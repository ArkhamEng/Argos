using Argos.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Production
{
    [Table("Service", Schema = "Production")]
    public class Service:Occurence
    {
        [Display(Name ="Tipo de Servicio")]
        public ServiceTypes ServiceTypeId { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }


        [Display(Name = "Fecha Termino")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CompletionDate { get; set; }

        public virtual ServiceType ServiceType { get; set; }

    }
}