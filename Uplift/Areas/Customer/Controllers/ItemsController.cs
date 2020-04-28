using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.Models;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data;
using Uplift.Utility;
using Uplift.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace Uplift.Controllers
{
    [Area("Customer")]
    //[Authorize(Roles = SD.Admin)]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;

        public ItemsController(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            dynamic ViewModel = new ExpandoObject();



            //  TROUBLE WITH CALLING THIS: Throws a "NullReferenceException: Object reference not set to an instance of an object." error: 
            // var OffersList = _unitOfWork.Offer.GetAll();

            // INSERT FUNCTION HERE TO PARSE THROUGH OFFERS WHERE OFFER.SellerID = Item.SellerID

            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }

            List<Offer> OffersList = new List<Offer>();
            Offer myOffer = new Offer();
            myOffer.buyerEmail = "BUYEREMAIL";
            myOffer.FName = "Ryland";
            myOffer.LName = "Hunstad";
            myOffer.ItemID = item.ItemID;
            myOffer.Email = "rhunstad@crimson.ua.edu";
            OffersList.Add(myOffer);

            ViewModel.item = item;
            ViewModel.user = user;

            //CHANGE THIS ONCE YOU UPDATE METHOD ABOVE: 
            ViewModel.Offers = OffersList;

            return View(ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String id)
        {
            

            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

            string[] nameArray = user.Name.Split(" ");
            Console.WriteLine(nameArray[0]);
            Console.WriteLine(nameArray[1]);

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemID == Guid.Parse(id));

            Offer newOffer = new Offer();
            newOffer.ItemID = item.ItemID;
            newOffer.buyerEmail = user.Email;
            newOffer.Email = item.Email;
            newOffer.SellerID = item.SellerID;
            newOffer.BuyerID = Guid.Parse(user.Id);
            newOffer.FName = nameArray[0];
            newOffer.LName = nameArray[1];
            newOffer.OfferDate = DateTime.Now;


            _context.Add(newOffer);
            await _context.SaveChangesAsync();
            

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
