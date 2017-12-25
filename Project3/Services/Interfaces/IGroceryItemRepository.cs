using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Services.Interfaces
{
    public interface IGroceryItemRepository
    {
		ICollection<GroceryItem> ReadAll();
		GroceryItem Create(GroceryItem groceryItem);
		GroceryItem Read(int id);
		void Update(GroceryItem groceryItem);
		void Delete(int id);
    }
}
