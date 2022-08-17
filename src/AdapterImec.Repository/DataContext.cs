using AdapterImec.Domain.Entities;
using AdapterImec.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdapterImec.Repository
{
    public class DataContext : DbContext
    {
        private const string AdapterImecConnectionString = "ConnectionString:AdapterImec";

        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = new ConfigurationBuilder().AddUserSecrets<DataContext>().Build();

            //var sqlConnectionString = configuration[AdapterImecConnectionString];
            //if (string.IsNullOrEmpty(sqlConnectionString))
            //{
            //    throw new ConfigurationException($"{AdapterImecConnectionString} is not set.");
            //}

            //if (!string.IsNullOrWhiteSpace(sqlConnectionString))
            //{
            //    optionsBuilder.UseNpgsql(sqlConnectionString).EnableSensitiveDataLogging(true).EnableDetailedErrors();
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration are int the Configuration folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfiguration).Assembly);
        }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            return new DataContext(optionsBuilder.Options);
        }
    }
}
