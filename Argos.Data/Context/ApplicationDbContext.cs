using Argos.Models;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SystemUser> SystemUsers { get; set; }

        public ApplicationDbContext()
            : base("SystemDB", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Security");
            base.OnModelCreating(modelBuilder);

        }
    }
}