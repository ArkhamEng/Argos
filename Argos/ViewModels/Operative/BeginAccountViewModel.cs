using Argos.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argos.ViewModels.Operative
{
    public class BeginAccountViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de un cliente registrado")]
        public int ClientId { get; set; }

        [Required(ErrorMessage ="Debe ingresar el nombre de un cliente registrado")]
        [Display(Name ="Nombre del Cliente")]
        public string ClientName { get; set; }

        
        public SelectList ServiceTypes { get; set; }

        [Required(ErrorMessage = "debe seleccionar el tipo de servicio")]
        [Display(Name ="Tipo de Servicio")]
        public int ServiceTypeId { get; set; }

        [Required(ErrorMessage = "debe ingresar la fecha de contratación")]
        [Display(Name ="Fecha de contrato")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Costo de contrato")]
        [Range(0,double.MaxValue,ErrorMessage ="El costo debe ser mayor a 0")]
        public int HirePrice { get; set; }

        public BeginAccountViewModel()
        {
            this.HireDate = DateTime.Now.ToLocal();
        }

    }
}