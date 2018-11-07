using Argos.Models.Business;
using Argos.Models.Purchasing;
using Argos.Models.Sales;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext
    {
        public DbSet<Detail> Details { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }

        public DbSet<SaleStatus> SaleStatus { get; set; }

        public DbSet<Sale> Sales { get; set; }

    }
}
