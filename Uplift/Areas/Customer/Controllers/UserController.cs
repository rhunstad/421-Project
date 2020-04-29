using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Uplift.Models;
using Microsoft.AspNetCore.Authorization;

namespace Uplift.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;



            dynamic userData = new ExpandoObject();
            var ItemsList = _unitOfWork.Item.GetAll();
            string[] nameArray = user.Name.Split(" ");


            List<Item> custItems = new List<Item>();
            
            var count = 0;
            // SUBSTITUTE THIS METHOD FOR A METHOD THAT FINDS ALL ITEMS WHERE Item.SellerID = customer.UserID

            foreach (var Item in ItemsList)
            {
                if(Item.SellerID == Guid.Parse(user.Id))
                {
                    custItems.Add(Item);
                }
                count += 1;
            }

            Customer cust = new Customer();
            cust.Email = user.Email;
            cust.Fname = nameArray[0];
            cust.LName = nameArray[1];
            cust.Username = user.UserName;
            cust.PhoneNumber = long.Parse(user.PhoneNumber);

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
