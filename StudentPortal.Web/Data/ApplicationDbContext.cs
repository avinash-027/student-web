using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using static Azure.Core.HttpHeader;
using System.Security.Policy;

namespace StudentPortal.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
             
        }

		// protected override void OnModelCreating(ModelBuilder modelBuilder)
		// {
		// 	base.OnModelCreating(modelBuilder);

		// 	// Fluent API vs. Conventions in EF Core
		// 	// 
		// 	// • Fluent API: This is where you explicitly configure how EF Core maps your models to the database schema. It's used when you want to customize or override the default behavior, such as specifying relationships, column names, or constraints.
		// 	//• Conventions: EF Core has built-in conventions that automatically configure relationships based on the names of your properties and navigation properties.If you follow these conventions, EF Core can figure out most relationships on its own, without needing Fluent API.
		// 	//
		// 	//
		// 	// Configuring one-to-one relationship
		// 	modelBuilder.Entity<Items>()
		// 	.HasOne(i => i.SerialNumber)
		// 	.WithOne(s => s.Items)
		// 	.HasForeignKey<SerialNumber>(s => s.ItemId)
		// 	.OnDelete(DeleteBehavior.Cascade);  // Example: set cascading delete
		// 	// ; // The foreign key is defined in the SerialNumber class

		// 	// Seeding data (Optional)
		// 	modelBuilder.Entity<Items>().HasData(
		// 	new Items { Id = 4, Name = "Microphone", price = 46, SerialNumberId = 10 });
		// 	modelBuilder.Entity<SerialNumber>().HasData(
		// 	new SerialNumber { Id = 10, Name = "MIC156", ItemId = 4 });
		// }
		public DbSet<Student> Students { get; set; }
        // public DbSet<Items> Items { get; set; }
        // public DbSet<SerialNumber> SerialNumbers { get; set; }
    }
}
