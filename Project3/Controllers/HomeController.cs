using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
		/// <summary>
		/// Redirects to the GroceryList index page
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
			return RedirectToAction("Index", "GroceryList");
		}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
