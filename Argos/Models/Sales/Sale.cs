using Argos.Models.BaseTypes;
using Argos.Models.Catalog;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argos.Models.Sales
{
    [Table("Sale", Schema = "Sales")]
    public class Sale:AuditableEntity
    {
        [Column(Order = 0)]
        public int SaleId { get; set; }

        [Column(Order = 1)]
        [MaxLength(10)]
        public string Folio { get; set; }

        [Column(Order = 2)]
        public DateTime SaleDate { get; set; }

        [Column(Order = 3)]
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }

        [Column(Order = 4)]
        public int ClientId { get; set; }

        [Column(Order = 5)]
        public int SaleStatusId { get; set; }

        [Column(Order = 6)]
        public int EmployeeId { get; set; }


        #region Navigation Properties

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public SaleStatus SaleStatus { get; set; }

        public virtual Commission Commission { get; set; }

        public  ICollection<SaleDetail> SaleDetails { get; set; }

        

        #endregion

    }
}