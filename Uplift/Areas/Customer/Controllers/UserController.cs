using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.Models;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            dynamic userData = new ExpandoObject();

            List<Item> custItems = new List<Item>();
            var displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            custItems.Add(displayItem);

            Customer cust = new Customer();

            cust.Email = "rhunstad@crimson.ua.edu";
            cust.Fname = "Ryland";
            cust.LName = "Hunstad";
            cust.username = "rylandhunstad";
            cust.phoneNumber = 720;

            userData.custItems = custItems;
            userData.customer = cust;

            return View(userData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
