using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.Entities
{
    public class GroceryListUser
    {
		public int GroceryListId { get; set; }
		public string UserId { get; set; }

		public GroceryList GroceryList { get; set; }
		public ApplicationUser User { get; set; }
    }
}
