﻿using System;
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

namespace Uplift.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class NewlistingController : Controller
    {

        private readonly ApplicationDbContext _context;

        public NewlistingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
