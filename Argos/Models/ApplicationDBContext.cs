using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Operative;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<EmployeeUser> EmployeeUsers { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ServiceStatus> ServiceStatus { get; set; }

        public DbSet<ServiceAccount> ServiceAccounts { get; set; }

        public DbSet<AccountAddress> AccountAddresses { get; set; }


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