using Microsoft.EntityFrameworkCore;

namespace ElectoralRegisterResidentInformationApi.V1.Infrastructure
{

    public class ElectoralRegisterContext : DbContext
    {
        public ElectoralRegisterContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Elector> Electors { get; set; }
        public DbSet<ElectorExtension> ElectorExtensions { get; set; }
        public DbSet<ElectorsProperty> Properties { get; set; }
    }
}
