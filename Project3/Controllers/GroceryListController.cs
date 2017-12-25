using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project3.Services.Interfaces;
using Project3.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Models.Entities;

namespace Project3.Controllers
{
	/// <summary>
	/// Controller Class to control grocery lists
	/// Implements the IGroceryListRepository, IGroceryItemRepository, and IGroceryListUsersRepository repositories
	/// Uses the UserManager
	/// </summary>
	[Authorize]
    public class GroceryListController : Controller
    {
		private IGroceryListRepository _groceryLists;
		private IGroceryItemRepository _groceries;
		private IGroceryListUsersRepository _groceryListUsers;
		private UserManager<ApplicationUser> _userManager;

		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="groceryLists"></param>
		/// <param name="groceries"></param>
		/// <param name="groceryListUsers"></param>
		/// <param name="userManager"></param>
		public GroceryListController(IGroceryListRepository groceryLists, IGroceryItemRepository groceries, IGroceryListUsersRepository groceryListUsers, UserManager<ApplicationUser> userManager)
		{
			_groceryLists = groceryLists;
			_groceries = groceries;
			_groceryListUsers = groceryListUsers;
			_userManager = userManager;
		}

		/// <summary>
		/// Displays all grocery lists that the user has access to
		/// </summary>
		/// <returns>Returns a view with GroceryListVM as the model</returns>
        public IActionResult Index()
        {
			//Get groceryListUsers where the userId = the current user's id
			var glu = _groceryListUsers.ReadAll().Where(g => g.UserId == _userManager.GetUserId(User))
				.Select( gl => new GroceryListVM
				{
					Id = gl.GroceryList.Id,
					Name = gl.GroceryList.Name,
					Groceries = gl.GroceryList.Groceries
				});

			return View(glu);
        }
		
		//Helper function
		private bool IsAjaxRequest()
		{
			return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
		}

		/// <summary>
		/// Return create view
		/// </summary>
		/// <returns></returns>
		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Creates a new grocery list item
		/// </summary>
		/// <param name="cglVM"></param>
		/// <returns></returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Create(CreateGroceryListVM cglVM)
		{
			if(ModelState.IsValid)
			{
				//Create a new grocery list
				var list = _groceryLists.Create(cglVM.CreateGroceryList());

				//Create new GroceryListUser
				GroceryListUser glu = new GroceryListUser
				{
					UserId = _userManager.GetUserId(User),
					GroceryListId = list.Id,
					GroceryList = _groceryLists.Read(list.Id),
					User = _groceryListUsers.ReadAllUsers().FirstOrDefault(u => u.Id == _userManager.GetUserId(User))
				};

				//Give access to the user who created it
				_groceryListUsers.Create(glu);
				list.GroceryListUsers.Add(glu);
				glu.User.GroceryListUsers.Add(glu);

				return RedirectToAction("Index");
			}
			return View(cglVM);
		}

		/// <summary>
		/// Returns the edit page for the specified id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns View with GroceryListVM as model</returns>
		public IActionResult Edit(int id)
		{
			//Read the specified list
			var list = _groceryLists.Read(id);

			if (list == null)
			{
				return RedirectToAction("Index");
			}
			//Get the user and groceryListUser
			var userId = _userManager.GetUserId(User);
			var glu = _groceryListUsers.Read(userId, id);

			if (glu == null)
			{
				return RedirectToAction("UnauthorizedAccess");
			}

			//Create GroceryListVM
			var listVM = new GroceryListVM
			{
				Id = list.Id,
				Name = list.Name,
				Groceries = list.Groceries
			};

			return View(listVM);
		}

		/// <summary>
		/// Updates a grocery list
		/// </summary>
		/// <param name="groceryList"></param>
		/// <returns>Redirects to index if list is updated</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Edit(GroceryListVM groceryList)
		{
			//Update the grocery list
			if(ModelState.IsValid)
			{
				_groceryLists.Update(groceryList.CreateGroceryList());
				return RedirectToAction("Index");
			}
			return View(groceryList);
		}

		/// <summary>
		/// Adds a grocery item on the edit page
		/// Uses AJAX to perform this method
		/// </summary>
		/// <param name="cgiVM"></param>
		/// <returns>Returns JSON</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult AddGrocery(CreateGroceryItemVM cgiVM)
		{
			//Create a grocery from the VM
			var grocery = cgiVM.CreateGroceryItem();
			//Read the list from the database
			var list = _groceryLists.Read(cgiVM.GroceryListId);
			grocery.GroceryList = list;

			//Create item with AJAX call
			if(ModelState.IsValid)
			{
				if (IsAjaxRequest())
				{
					_groceries.Create(grocery);
					list.Groceries.Add(grocery);
					return Json("Ok");
				}

			} else
			{
				return Json("Not Okay");
			}
			return RedirectToAction("Index", "GroceryList");
		}

		/// <summary>
		/// Removes grocery from a list on the edit page
		/// </summary>
		/// <param name="id"></param>
		/// <param name="listId"></param>
		/// <returns>Json</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult RemoveGrocery(int id, int listId)
		{
			if (IsAjaxRequest())
			{
				//Read grocery from database based on its id and remove it with AJAX
				var grocery = _groceries.Read(id);
				var list = _groceryLists.Read(listId);
				list.Groceries.Remove(grocery);
				_groceries.Delete(id);
				return Json("Okay");
			}
			return Json("Not Okay");
		}

		/// <summary>
		/// Shows details page - all groceries on a list and if it is checked off
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public IActionResult Details(int id)
		{
			//Read a grocery list from the databse
			var list = _groceryLists.Read(id);

			if (list == null)
			{
				return RedirectToAction("Index");
			}

			//Get the user and groceryListUser
			var userId = _userManager.GetUserId(User);
			var glu = _groceryListUsers.Read(userId, id);

			if (glu == null)
			{
				return RedirectToAction("UnauthorizedAccess");
			}

			//Create GroceryListVM
			var listVM = new GroceryListVM
			{
				Id = list.Id,
				Name = list.Name,
				Groceries = list.Groceries
			};
			return View(listVM);
		}

		/// <summary>
		/// Checks off items on details page
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Json</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult CheckOffItem(int id)
		{
			if (IsAjaxRequest())
			{
				//Read grocery from databse and then update its checkedOff property
				var grocery = _groceries.Read(id);
				grocery.IsCheckedOff = !grocery.IsCheckedOff;
				_groceries.Update(grocery);
				return Json("Okay");
			}
			return Json("Not Okay");
		}

		/// <summary>
		/// Shows the permissions page of the specified list
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Permission page with a PermissionsVM as the model</returns>
		public IActionResult Permissions(int id)
		{
			//Find grocery list in database
			var list = _groceryLists.Read(id);

			//get all users
			var groceryListUsers = _groceryListUsers.ReadAllUsers();

			if (list == null)
			{
				return RedirectToAction("Index");
			}

			//Get the user's id and read the user
			var userId = _userManager.GetUserId(User);
			var glu = _groceryListUsers.Read(userId, id);

			if (glu == null)
			{
				return RedirectToAction("UnauthorizedAccess");
			}

			//Get all groceryListUsers that have access where the list id is = to the selected list id and create UserListVM
			var users = _groceryListUsers.ReadAll().Where(l => l.GroceryListId == list.Id).Select(u => new UserListVM
			{
				Id = u.User.Id,
				UserName = u.User.UserName,
				FirstName = u.User.FirstName,
				LastName = u.User.LastName
			});

			//Get all groceryListUsers
			var allGroceryListUsers = _groceryListUsers.ReadAll().Where(l => l.GroceryListId == list.Id);

			//Get all the users that do not have access to the selected list
			var usersWithoutAccess = _groceryListUsers.ReadAllUsers()
				.Where(u => !allGroceryListUsers.Any(u2=> u2.UserId == u.Id))
				.Select(uiVM => new UserInfoVM
				{
					Id = uiVM.Id,
					Username = uiVM.UserName
				});

			//Create a new permissionsVM
			var permissionsVM = new PermissionsVM
			{
				ListId = list.Id,
				ListName = list.Name,
				Users = users,
				UsersWithoutAccess = usersWithoutAccess
			};
			return View(permissionsVM);
		}

		/// <summary>
		/// Grants permission to a specified user
		/// </summary>
		/// <param name="gpVM"></param>
		/// <returns>Json</returns>
		public IActionResult GrantPermission(GrantPermissionVM gpVM)
		{
			//get the user from the databse
			var user = _groceryListUsers.ReadUser(gpVM.SelectedUserId);
			//Read the selected list from the database
			var list = _groceryLists.Read(gpVM.ListId);
			if(user == null || list == null)
			{
				return Json("Failed");
			} else
			{
				//Create a new GroceryListUser
				var glu = new GroceryListUser
				{
					GroceryList = list,
					User = user,
					GroceryListId = list.Id,
					UserId = user.Id
				};
				//Add access in the database
				var groceryListUser = _groceryListUsers.Create(glu);
				groceryListUser.User.GroceryListUsers.Add(groceryListUser);
				groceryListUser.GroceryList.GroceryListUsers.Add(groceryListUser);
				return Json("Okay");
			}

		}

		/// <summary>
		/// Revokes priveleges to specified user - AJAX is used to do this
		/// </summary>
		/// <param name="id"></param>
		/// <param name="listId"></param>
		/// <returns>Json</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Revoke(string id, int listId)
		{
			if (IsAjaxRequest())
			{
				//Revoke access in the database
				var glu = _groceryListUsers.Read(id, listId);
				_groceryListUsers.Delete(id, listId);
				return Json("Okay");
			}
			return Json("Not Okay");
		}

		/// <summary>
		/// Displays the Unauthorized Access page
		/// </summary>
		/// <returns>View</returns>
		public IActionResult UnauthorizedAccess()
		{
			return View();
		}
    }
}