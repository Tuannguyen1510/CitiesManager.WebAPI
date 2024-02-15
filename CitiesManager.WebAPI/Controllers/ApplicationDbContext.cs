using CitiesManager.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CitiesManager.WebAPI.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }

		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // add data 
            builder.Entity<City>().HasData(new City() { Id = Guid.Parse("A94B7FEC-B2C7-4F8D-9AA4-DD282FAD08BF"), Name ="City" });
			builder.Entity<Employee>().HasData(new Employee() { EmployeeId = Guid.Parse("1CB09EE6-659F-4B18-9F46-87B22A0DF831"), EmployeeName = "A" , PhotoFileName="hinh.pgn" });

		}


	}
}
