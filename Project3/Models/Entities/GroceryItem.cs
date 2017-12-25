using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.Entities
{
    public class GroceryItem
    {
		public int Id { get; set; }
		[Required]
		public string GroceryName { get; set; }
		[Required]
		public decimal GroceryAmount { get; set; }
		[Required]
		public bool IsCheckedOff { get; set; }

		public int GroceryListId { get; set; }
		public GroceryList GroceryList { get; set; }
    }
}
