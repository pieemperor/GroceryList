using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.ViewModels
{
	public class PermissionsVM
	{
		public int ListId { get; set; }
		public string ListName { get; set; }
		public IQueryable<UserListVM> Users { get; set; }
		public IQueryable<UserInfoVM> UsersWithoutAccess { get; set; }
    }
}
