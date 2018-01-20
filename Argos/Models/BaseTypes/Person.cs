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
        [MaxLength(100)]
        public string Name { get; set; }

        //Federal Taxpayer register
        [Display(Name = "R.F.C.")]
        [MaxLength(15)]
        [Index("Unq_FTR", IsUnique = true)]
        public string FTR { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        [Index("Unq_Phone", IsUnique = true)]
        public string Phone { get; set; }


        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(150)]
        [Index("Unq_Email", IsUnique = true)]
        public string Email { get; set; }

    }
}