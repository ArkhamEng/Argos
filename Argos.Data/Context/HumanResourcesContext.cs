using Argos.Models.Business;
using Argos.Models.Config;
using Argos.Models.HumanResources;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext
    {
        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<EmployeeBranch> EmployeeBranches { get; set; }
    }
}
