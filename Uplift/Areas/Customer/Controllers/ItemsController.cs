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
using System.Dynamic;

namespace Uplift.Controllers
{
    [Area("Customer")]
    //[Authorize(Roles = SD.Admin)]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            dynamic ViewModel = new ExpandoObject();

            //  TROUBLE WITH CALLING THIS: Throws a "NullReferenceException: Object reference not set to an instance of an object." error: 
            // var OffersList = _unitOfWork.Offer.GetAll();

            // INSERT FUNCTION HERE TO PARSE THROUGH OFFERS WHERE OFFER.SellerID = Item.SellerID

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

            //CHANGE THIS ONCE YOU UPDATE METHOD ABOVE: 
            ViewModel.Offers = OffersList;

            return View(ViewModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
