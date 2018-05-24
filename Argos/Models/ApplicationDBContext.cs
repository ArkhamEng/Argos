using Argos.Models.HumanResources;
using Argos.Models.Config;
using Argos.Models.Inventory;
using Argos.Models.Production;
using Argos.Models.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Argos.Models.Operative;
using Argos.Models.BusinessEntity;
using Argos.Models.Finance;

namespace Argos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region BussinessEntity
        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<PhoneType> PhoneTypes { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }
        #endregion

        #region Config
        public DbSet<State> States { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<Maker> Makers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Compatibility> Compatibilities { get; set; }

        public DbSet<Configuration> Configurations { get; set; }


        #endregion

        #region Operative

        public DbSet<ShipMethod> ShipMethods { get; set; }

        public DbSet<Shipping> Shippings { get; set; }

        public DbSet<OperationStatus> OperationStatus { get; set; }

        public DbSet<OperationType> OperationType { get; set; }

     
        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationDetail> OperationDetails { get; set; }


        #endregion

        #region HumanResources
      
        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        public DbSet<EmployeeBranch> EmployeeBranches { get; set; }

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


        #region Finance

        public DbSet<CashRegister> CashRegisters { get; set; }

        public DbSet<CashSession> CashSessions { get; set; }

        public DbSet<PayMethod> PayMethodes { get; set; }
        
        public DbSet<CashMovement> CashMovement { get; set; }

        public DbSet<CreditNote> CreditNotes { get; set; }


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