using Argos.Models.Business;
using System.Data.Entity;

namespace Argos.Data.Context
{
    public partial class ApplicationDbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<PhoneType> PhoneTypes { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<EmailType> EmailTypes { get; set; }
    }
}
