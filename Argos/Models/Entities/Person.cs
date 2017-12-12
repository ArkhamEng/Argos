using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Entities
{
    public class Person
    {
        [Display(Name = "Clave")]
        [Required]
        [MaxLength(10)]
        [Index("Unq_Code", IsUnique = true)]
        public string Code { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

      
        //Federal Taxpayer register
        [Display(Name = "R.F.C.")]
        [MaxLength(15)]
        [Index("Unq_FTR", IsUnique = true)]
        public string FTR { get; set; }


        [Display(Name = "Calle y numero")]
        [MaxLength(150)]
        [Required]
        public string Street { get; set; }

        [Display(Name = "Colonia")]
        [MaxLength(150)]
        [Required]
        public string Location { get; set; }

        [Display(Name = "C.P.")]
        [MaxLength(10)]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Entrance { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Index("Unq_Email", IsUnique = true)]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }

        [Display(Name = "Municipio")]
        [Required]
        public int CityId { get; set; }

        [MaxLength(100)]
        public string UpdUser { get; set; }

        [Required]
        public DateTime UpdDate { get; set; }

        public virtual City City { get; set; }

        public Person()
        {
            this.Entrance = DateTime.Now;
            this.UpdDate  = DateTime.Now;
        }
    }
}