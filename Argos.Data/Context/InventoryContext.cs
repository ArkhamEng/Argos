using Argos.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Data.Context
{
   public partial class ApplicationDbContext
    {
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
    }
}
