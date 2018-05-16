using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Models.Production;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Argos.Models.Operative;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Operative

        public DbSet<Client> Clients { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<ShipMethod> ShipMethods { get; set; }

        public DbSet<Shipping> Shippings { get; set; }

        public DbSet<OperationStatus> OperationStatus { get; set; }

        public DbSet<OperationType> OperationType { get; set; }

        public DbSet<OperationChange> OperationChanges { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationDetail> OperationDetails { get; set; }


        #endregion

        #region HumanResources
        public DbSet<Employee> Employees { get; set; }

        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        #endregion

        #region Inventory

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ExternalProduct> ExternalProduct { get; set; }

        public DbSet<PriceChange> PriceHistories { get; set; }

        public DbSet<ProductStock> ProductStocks { get; set; }

        public DbSet<MeasureUnit> MeasureUnits { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Serial> Serials { get; set; }

        public DbSet<StockMovement> StockMovements { get; set; }

        #endregion

        #region Config
        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<Maker> Makers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Compatibility> Compatibilities { get; set; }

        public DbSet<Configuration> Config { get; set; }


        #endregion

        #region Production

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountFile> AccountFiles { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Service> Activity { get; set; }

        public DbSet<AccountLocation> Locations { get; set; }

        public DbSet<Production.OperativeStatus> OperativeStatus { get; set; }

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