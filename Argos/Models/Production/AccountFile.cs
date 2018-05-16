using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.Production
{
    [Table("AccountFile", Schema = "Production")]
    public class AccountFile
    {
        public int AccountFileId { get; set; }

        public int AccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Path { get; set; }

        public int Size { get; set; }

        public DateTime InsDate { get; set; }

        [MaxLength(30)]
        public string InsUser { get; set; }


        #region Navigation Properties

        public virtual Account Service { get; set; }
        #endregion
    }
}