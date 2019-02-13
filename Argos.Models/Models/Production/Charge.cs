using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Production
{
    [Table("Charge", Schema = "Production")]
    public class Charge:Occurence
    {
        [Display(Name ="Fecha de pago")]
        [DataType(DataType.DateTime)]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Importe")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

    }
}
