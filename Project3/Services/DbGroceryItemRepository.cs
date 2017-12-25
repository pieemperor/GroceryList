using Project3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Project3.Data;

namespace Project3.Services
{
    public class DbGroceryItemRepository : IGroceryItemRepository
    {
		private ApplicationDbContext _db;

		public DbGroceryItemRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public GroceryItem Create(GroceryItem groceryItem)
		{
			_db.Groceries.Add(groceryItem);
			_db.SaveChanges();
			return groceryItem;
		}

		public void Delete(int id)
		{
			GroceryItem groceryItem = _db.Groceries.Find(id);
			_db.Remove(groceryItem);
			_db.SaveChanges();
		}

		public GroceryItem Read(int id)
		{
			return _db.Groceries.FirstOrDefault(g => g.Id == id);
		}

		public ICollection<GroceryItem> ReadAll()
		{
			return _db.Groceries.ToList();
		}

		public void Update(GroceryItem groceryItem)
		{
			_db.Entry(groceryItem).State = EntityState.Modified;
			_db.SaveChanges();
		}
	}
}
