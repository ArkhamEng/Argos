using Argos.Models.Catalog;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Models.Operative;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Catalog
        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Supplier> Providers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        #endregion

        #region Config
        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<CarMake> CarMakes { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<CarYear> CarYears { get; set; }

        public DbSet<MeasureUnit> MeasureUnits { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        #endregion

        #region Operative

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<ServiceAccount> ServiceAccounts { get; set; }

        public DbSet<AccountAddress> AccountAddresses { get; set; }


        #endregion

        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<EmployeeUser> EmployeeUsers { get; set; }

     


     

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