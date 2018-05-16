﻿using Argos.Models.BaseTypes;
using Argos.Models.HumanResources;
using Argos.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Argos.Support;
using Argos.Models.Finance;
using Argos.Models.Enums;

namespace Argos.Models.Operative
{
    [Table("Sale", Schema = "Operative")]
    public class Sale: Operation
    {

        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("Type")]
        public OpType TypeId { get; set; }

        [ForeignKey("Status")]
        public OpStatus StatusId { get; set; }

        [MaxLength(10)]
        public string SaleCode { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime? DueDate { get; set; }

        [Display(Name = "IVA")]
        public double Tax { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        [Display(Name = "Monto IVA")]
        [DataType(DataType.Currency)]
        public double TaxAmount { get; set; }

        [Display(Name = "Monto Total")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        [Index("IDX_Sequence", 1, IsUnique = true)]
        public int Year { get; set; }

        [Index("IDX_Sequence", 2, IsUnique = true)]
        public int Month { get; set; }

        [Index("IDX_Sequence", 3, IsUnique = true)]
        public int Sequential { get; set; }

        [Display(Name ="Requiere envío")]
        public bool ForShipping { get; set; }

        public Sale()
        {
            this.Year = Convert.ToInt32(DateTime.Now.TodayLocal().ToString("yy"));
            this.Month = DateTime.Now.TodayLocal().Month;
        }

        public void ApplyTaxes()
        {
            if (Tax > Cons.Zero && SubTotal > Cons.Zero)
            {
                this.Total = (SubTotal * (Cons.One + (this.Tax / Cons.OneHundred))).RoundMoney();
                this.TaxAmount = (this.Total - this.SubTotal).RoundMoney();
            }
        }

        #region Navigation Properties

        public virtual OperationStatus Status { get; set; }

        public virtual OperationType Type { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Client Client { get; set; }

        public virtual Commission Commission { get; set; }

        public virtual Shipping Shipping { get; set; }

        public ICollection<FinantialMovement> FinantialMovements { get; set; }
        #endregion

    }
}