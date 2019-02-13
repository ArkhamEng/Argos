using Argos.Models.Production;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext
    {
        public DbSet<AccountStatus> AccountStatuses { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountHistory> AccountHistories { get; set; }

        public DbSet<Occurence> Occurences { get; set; }

        public DbSet<Charge> Charges { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<MaintPeriod> MaintPeriodes { get; set; }

        public DbSet<PaymentPeriod> PaymentPeriodes { get; set; }
    }
}
