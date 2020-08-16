using DataAccess.Database.EntityConfiguration;
using Models;
using System.Data.Entity;

namespace DataAccess.Database
{
	public class DataContext : DbContext
    {
		public DataContext() : base("name=MyDbConnection")
		{
			Configuration.LazyLoadingEnabled = false;
			Configuration.ValidateOnSaveEnabled = false;
			Configuration.EnsureTransactionsForFunctionsAndCommands = false;
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserConfiguration());
		}
	}
}
