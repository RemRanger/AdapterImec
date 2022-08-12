using Microsoft.EntityFrameworkCore;
using System;

namespace AdapterImec.Repository
{
    internal class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly DataContext dataContext;

        public DatabaseMigrator(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void Init()
        {
            try
            {
                dataContext.Database.Migrate();
                dataContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }

    public interface IDatabaseMigrator
    {
        void Init();
    }
}
