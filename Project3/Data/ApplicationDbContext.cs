using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project3.Models;
using Project3.Models.Entities;

namespace Project3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<GroceryListUser>()
				   .HasKey(glu => new { glu.GroceryListId, glu.UserId});

		}


		public DbSet<GroceryItem> Groceries { get; set; }
		public DbSet<GroceryList> GroceryLists { get; set; }
		public DbSet<GroceryListUser> GroceryListUsers { get; set; }
	}
}
