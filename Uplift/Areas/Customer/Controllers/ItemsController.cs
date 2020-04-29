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
    [Authorize]
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
            var OffersFirst = _unitOfWork.Offer.GetAll();

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

            var count = 0;

            foreach (var Offer in OffersFirst)
            {
                if (Offer.ItemID == item.ItemID)
                {
                    OffersList.Add(Offer);
                }
                count += 1;
            }
            Console.WriteLine();

            ViewModel.item = item;
            ViewModel.user = user;

            ViewModel.Offers = OffersList;

            return View(ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String id)
        {
            Console.WriteLine();

            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

            string[] nameArray = user.Name.Split(" ");

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

            Console.WriteLine();
            _context.Add(newOffer);
            await _context.SaveChangesAsync();

            var NewUrl = "https://xchangewebsite.azurewebsites.net/Customer/Items/Index/" + id;
            return Redirect(NewUrl);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
