using Microsoft.EntityFrameworkCore;
using TestTaskForUniversalExchange.Models;

namespace TestTaskForUniversalExchange.EF
{
	public class UniversalExchangeContext : DbContext
	{
		public DbSet<Application> Applications { get; set; }

		public UniversalExchangeContext(DbContextOptions<UniversalExchangeContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
