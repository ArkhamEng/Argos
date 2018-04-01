using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Models.Operative;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Argos.Models.Transaction;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Transaction

        public DbSet<Client> Clients { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleDetail> SaleDetails { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<ShipMethod> ShipMethodes { get; set; }

        public DbSet<Shipping> Shipping { get; set; }

        public DbSet<Transaction.TransStatus> TransStatus { get; set; }

        public DbSet<TransType> TransType { get; set; }



        #endregion

        #region HumanResources
        public DbSet<Employee> Employees { get; set; }

        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        #endregion

        #region Inventory

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Compatibility> Compatibilities { get; set; }

        public DbSet<Equivalence> Equivalences { get; set; }

        public DbSet<ExternalProduct> ExternalProduct { get; set; }

        public DbSet<PackageDetail> PackageDetail { get; set; }

        public DbSet<PriceHistory> PriceHistories { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<MeasureUnit> MeasureUnits { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        #endregion

        #region Config
        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<CarMake> CarMakes { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<CarYear> CarYears { get; set; }

        public DbSet<Configuration> Config { get; set; }


        #endregion

        #region Operative

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountFile> AccountFiles { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Service> Activity { get; set; }

        public DbSet<AccountLocation> Locations { get; set; }

        public DbSet<Operative.OperativeStatus> OperativeStatus { get; set; }

        #endregion

        #region Security

        public DbSet<EmployeeUser> EmployeeUsers { get; set; }

        public DbSet<ClientUser> ClientUsers { get; set; }

        #endregion




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