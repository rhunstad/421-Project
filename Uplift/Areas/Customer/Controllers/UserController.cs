using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.DataAccess.Data.Repository;
using Uplift.Models;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            dynamic userData = new ExpandoObject();
            var ItemsList = _unitOfWork.Item.GetAll();

            List<Item> custItems = new List<Item>();
            var displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            custItems.Add(displayItem);

            var count = 0;
            // SUBSTITUTE THIS METHOD FOR A METHOD THAT FINDS ALL ITEMS WHERE Item.SellerID = customer.UserID

            foreach (var Item in ItemsList)
            {
                if(count < 4)
                {
                    custItems.Add(Item);
                }
                count += 1;
            }

            Customer cust = new Customer();
            cust.Email = "rhunstad@crimson.ua.edu";
            cust.Fname = "Ryland";
            cust.LName = "Hunstad";
            cust.Username = "rylandhunstad";
            cust.PhoneNumber = 720;

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
