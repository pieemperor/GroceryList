using Project3.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.ViewModels
{
    public class CreateGroceryListVM
    {
		[Required]
		public string Name { get; set; }

		public GroceryList CreateGroceryList()
		{
			return new GroceryList
			{
				Name = Name
			};
		}
	}
}
