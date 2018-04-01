using Argos.Models.Config;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.BaseTypes
{
    public abstract class Person:LocatableEntity
    {
        [Display(Name = "Nombre")]
        [Required]
        [MaxLength(150)]
        [Index("IDX_Name", IsUnique = false)]
        public string Name { get; set; }

        //Federal Taxpayer register
        [Display(Name = "R.F.C.")]
        [MaxLength(13)]
        [Index("Unq_FTR", IsUnique = true)]
        public string FTR { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato admitido es (229) 123 45 67")]
        [MaxLength(15)]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }


        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress,ErrorMessage ="El e-mail no tiene un formato correcto")]
        [MaxLength(150)]
        [Index("Unq_Email", IsUnique = true)]
        public string Email { get; set; }

    }
}