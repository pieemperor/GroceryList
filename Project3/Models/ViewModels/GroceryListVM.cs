using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.ViewModels
{
    public class GroceryListVM
    {
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<GroceryItem> Groceries { get; set; }

		public GroceryList CreateGroceryList()
		{
			return new GroceryList
			{
				Id = Id,
				Name = Name,
				Groceries = Groceries
			};
		}
	}
}
