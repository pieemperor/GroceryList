using Project3.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Project3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Project3.Data;

namespace Project3.Services
{
	public class DbGroceryListRepository : IGroceryListRepository
    {
		private ApplicationDbContext _db;

		public DbGroceryListRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public GroceryList Create(GroceryList groceryList)
		{
			_db.GroceryLists.Add(groceryList);
			_db.SaveChanges();
			return groceryList;
		}

		public void Delete(int id)
		{
			GroceryList groceryList = _db.GroceryLists.Find(id);
			_db.GroceryLists.Remove(groceryList);
			_db.SaveChanges();
		}

		public GroceryList Read(int id)
		{
			return _db.GroceryLists.Include(gl => gl.Groceries).FirstOrDefault(gl => gl.Id == id);
		}

		public ICollection<GroceryList> ReadAll()
		{
			return _db.GroceryLists.Include(gl => gl.Groceries).ToList();
		}

		public void Update(GroceryList groceryList)
		{
			_db.Entry(groceryList).State = EntityState.Modified;
			_db.SaveChanges();
		}
	}
}
