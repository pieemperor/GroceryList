using Project3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project3.Models.Entities;
using Project3.Data;
using Project3.Models;
using Microsoft.EntityFrameworkCore;

namespace Project3.Services
{
	public class DbGroceryListUsersRepository : IGroceryListUsersRepository
	{
		ApplicationDbContext _db;

		public DbGroceryListUsersRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public GroceryListUser Create(GroceryListUser groceryListUsers)
		{
			_db.GroceryListUsers.Add(groceryListUsers);
			_db.SaveChanges();
			return groceryListUsers;
		}

		public GroceryListUser Read(string userId, int listId)
		{
			return _db.GroceryListUsers.FirstOrDefault(glu => glu.UserId.Equals(userId) && glu.GroceryListId == listId);
		}

		public ApplicationUser ReadUser(string id)
		{
			return _db.Users.FirstOrDefault(u => u.Id == id);
		}

		public void Delete(string id, int listId)
		{
			var groceryListUser = _db.GroceryListUsers.FirstOrDefault(glu => glu.UserId.Equals(id) && glu.GroceryListId == listId);
			_db.GroceryListUsers.Remove(groceryListUser);
			_db.SaveChanges();
		}

		public IQueryable<ApplicationUser> ReadAllUsers()
		{
			return _db.Users.Include(r => r.GroceryListUsers);
		}

		public IQueryable<GroceryListUser> ReadAll()
		{
			return _db.GroceryListUsers.Include(glu => glu.GroceryList).Include(glu => glu.User);
		}
	}
}
