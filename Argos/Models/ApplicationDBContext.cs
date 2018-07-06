using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Models.HumanResources;
using Argos.Models.Inventory;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region BussinessEntity
        public DbSet<Entity> Entities { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<PhoneType> PhoneTypes { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<EmailType> EmailTypes { get; set; }
        #endregion

        #region Config
        public DbSet<State> States { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<Maker> Makers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Compatibility> Compatibilities { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<CreditStatus> CreditStatus { get; set; }

        public DbSet<PayForm> PayForms { get; set; }

        public DbSet<PayMethod> PayMethods { get; set; }


        #endregion

        #region Purchasing & Sales

        public DbSet<Detail> Details { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Purchasing.PurchaseStatus> PurchaseStatus { get; set; }

        public DbSet<Sales.SaleStatus> SaleStatus { get; set; }

        public DbSet<Sale> Sales { get; set; }

        #endregion


        #region HumanResources

        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<EmployeeBranch> EmployeeBranches { get; set; }

        #endregion

        #region Inventory
        public DbSet<MeasureUnit> MeasureUnits { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductChange> ProductChanges { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Storage> Locations { get; set; }

        public DbSet<ItemStorage> ItemLocations { get; set; }

        public DbSet<Flow> Flows { get; set; }

        public DbSet<FlowDirection> FlowDirections { get; set; }

        public DbSet<SupplierProduct> SupplierProducts { get; set; }

        #endregion


        #region Security
        
        public DbSet<SystemUser> SystemUsers { get; set; }

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