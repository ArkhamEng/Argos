using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Models.Security;
using Argos.Common.Constants;


namespace Argos.Models.Business
{
    [Table("Person", Schema = "Business")]
    public abstract class Person : Entity
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

        public string ImagePath { get; set; }

        public string UserName
        {
            get { return SystemUser != null ? SystemUser.User.UserName : Labels.NoUser; }
        }

        public bool CanCreateUser { get { return (SystemUser != null); } }


        #region Navigation Properties

        public virtual SystemUser SystemUser { get; set; }

        #endregion
    }
}