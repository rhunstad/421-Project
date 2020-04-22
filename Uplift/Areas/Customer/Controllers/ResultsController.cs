using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.Models;
using Uplift.DataAccess.Data;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class ResultsController : Controller
    {
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(ILogger<ResultsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Item> SearchResultsItems = new List<Item>();
            var displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            SearchResultsItems.Add(displayItem);
            return View(SearchResultsItems);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
