using Argos.Models.Business;
using Argos.Models.Config;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext
    {
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
    }
}
