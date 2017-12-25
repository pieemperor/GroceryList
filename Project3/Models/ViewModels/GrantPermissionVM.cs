using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Models.ViewModels
{
    public class GrantPermissionVM
    {
		public int ListId { get; set; }
		public IQueryable<UserInfoVM> Users { get; set; }
		public string SelectedUserId { get; set; }
    }
}
