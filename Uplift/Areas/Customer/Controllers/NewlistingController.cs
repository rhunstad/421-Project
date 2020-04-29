using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data;
using Uplift.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Uplift.Controllers
{
    [Area("Customer")]
    
    public class NewlistingController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewlistingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create(string id)
        {
            /*
            The URL router has a value of id being passed into this controller.
            Because we may be passing in full GUID's as well as "null" values and "0" values it will be best
            to pass the id in as a string, check if it's a GUID and if it is cast id as type GUID
            */
            return View();
        }
        
        public async Task<IActionResult> GetItemPhoto(Guid id)
        {
            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (item == null)
            {
                return NotFound();
            }
            var imageData = item.ItemImage;

            return File(imageData, "image/jpg");
            /*
            if (imageData != null)
            {
                return File(imageData, "image/jpg");
            } else
            {
                return NotFound();
            }
            */
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Item newItem, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await imageFile.CopyToAsync(memoryStream);
                    newItem.ItemImage = memoryStream.ToArray();
                }

                var userId = _userManager.GetUserId(HttpContext.User);
                ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

                newItem.Email = user.Email;
                newItem.SellerID = Guid.Parse(user.Id);

                _context.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(newItem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
