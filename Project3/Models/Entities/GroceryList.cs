using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.Entities
{
    public class GroceryList
    {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public ICollection<GroceryItem> Groceries { get; set; }
		public ICollection<GroceryListUser> GroceryListUsers { get; set; }
	}
}
