using Project3.Models;
using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Services.Interfaces
{
    public interface IGroceryListUsersRepository
    {
		GroceryListUser Create(GroceryListUser groceryListUsers);
		GroceryListUser Read(string userId, int listId);
		ApplicationUser ReadUser(string id);
		void Delete(string id, int listId);
		IQueryable<ApplicationUser> ReadAllUsers();
		IQueryable<GroceryListUser> ReadAll();
	}
}
