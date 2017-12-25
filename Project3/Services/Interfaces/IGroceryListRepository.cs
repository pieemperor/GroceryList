using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Services.Interfaces
{
    public interface IGroceryListRepository
    {
		ICollection<GroceryList> ReadAll();
		GroceryList Create(GroceryList groceryList);
		GroceryList Read(int id);
		void Update(GroceryList groceryList);
		void Delete(int id);
	}
}
