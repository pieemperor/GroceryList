using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.ViewModels
{
    public class CreateGroceryItemVM
    {
		[Required]
		[Display(Name = "Item Name")]
		public string GroceryName { get; set; }
		[Required]
		[Display(Name = "Cost")]
		public decimal GroceryAmount { get; set; }

		public int GroceryListId { get; set; }
		public GroceryItem CreateGroceryItem()
		{
			return new GroceryItem
			{
				GroceryName = GroceryName,
				GroceryAmount = GroceryAmount,
				GroceryListId = GroceryListId
			};
		}
	}
}
