using Argos.Models.BaseTypes;
using Argos.Models.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Config
{
    [Table("CarMake", Schema = "Config")]
    public class CarMake:AuditableEntity,ISelectable
    {
        public int CarMakeId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<CarModel> CarModels { get; set; }

        #region Not Mapped

        [NotMapped]
        public string Value
        {
            get
            {
                return this.CarMakeId.ToString();
            }
        }

        [NotMapped]
        public string Text
        {
            get
            {
                return this.Name;
            }
        }
        #endregion
    }

    [Table("CarModel", Schema = "Config")]
    public class CarModel : AuditableEntity,ISelectable
    {
        [Column(Order =0)]
        public int CarModelId { get; set; }

        [Column(Order = 1)]
        public int CarMakeId { get; set; }

        [Column(Order = 2)]
        [MaxLength(100)]
        public string Name { get; set; }

        #region Navigation Properties
        public virtual CarMake CarMake  { get; set; }

        public ICollection<CarYear> CarYears { get; set; }
        #endregion

        #region Not Mapped

        [NotMapped]
        public string Value
        {
            get
            {
                return this.CarModelId.ToString();
            }
        }

        [NotMapped]
        public string Text
        {
            get
            {
                return this.Name;
            }
        }
        #endregion
    }

    [Table("CarYear", Schema = "Config")]
    public class CarYear:AuditableEntity,ISelectable
    {
        [Column(Order =0)]
        public int CarYearId { get; set; }

        [Column(Order = 1)]
        public int CarModelId { get; set; }

        [Column(Order = 2)]
        public int Year { get; set; }

        #region Not Mapped
        [NotMapped]
        public string Value
        {
            get
            {
                return this.CarYearId.ToString();
            }
        }

        [NotMapped]
        public string Text
        {
            get
            {
                return this.Year.ToString();
            }
        }
        #endregion

        #region Navigation Properties

        public virtual CarModel CarModel { get; set; }

        public ICollection<Compatibility> Compatibilities { get; set; }

        #endregion

    }
}