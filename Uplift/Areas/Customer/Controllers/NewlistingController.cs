using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.DataAccess.Data.Repository;
using Uplift.Models;

namespace Uplift.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class NewlistingController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public NewlistingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item newItem)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Item.Add(newItem);
                _unitOfWork.Save();
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
