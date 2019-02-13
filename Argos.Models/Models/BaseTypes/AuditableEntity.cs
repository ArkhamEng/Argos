using Argos.Common;
using Argos.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Argos.Models.BaseTypes
{
    public abstract class AuditableEntity
    {
        [Required]
        [Display(Name ="Fecha Registro")]
        public DateTime InsDate { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Registrado por")]
        public string InsUser { get; set; }

        [Required]
        [Display(Name = "Fecha Edición")]
        public DateTime UpdDate { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Editado por")]
        public string UpdUser { get; set; }


        public AuditableEntity()
        {
            this.InsDate = DateTime.Now.ToLocal();
            this.UpdDate = DateTime.Now.ToLocal();
            try
            {
                this.InsUser = HttpContext.Current.User.Identity.Name;
                this.UpdUser = HttpContext.Current.User.Identity.Name;
            }
            catch (Exception)
            {
                this.InsUser = "System";
                this.UpdUser = "System";
            }
        }
    }

    public abstract class ActivableAudit: AuditableEntity
    {
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public ActivableAudit()
        {
            this.IsActive = true;
        }
    }

}