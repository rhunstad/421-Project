using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.DataAccess.Data;
using Uplift.Models;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            dynamic ItemsList = new ExpandoObject();

            List<Item> FeaturedItems = new List<Item>();
            var displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            FeaturedItems.Add(displayItem);

            displayItem = new Item();
            displayItem.Title = "Title2";
            displayItem.Price = 79.99;
            displayItem.ItemDescription = "This is the item description2. This is the item description";
            FeaturedItems.Add(displayItem);

            displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            FeaturedItems.Add(displayItem);

            displayItem = new Item();
            displayItem.Title = "Title2";
            displayItem.Price = 79.99;
            displayItem.ItemDescription = "This is the item description2. This is the item descriptionThis is the item description3. This is the item descriptionThis is the item description4. This is the item descriptionThis is the item description5. This is the item descriptionThis is the item description6. This is the item descriptionThis is the item description7. This is the item descriptionThis is the item description8. This is the item descriptionThis is the item description9. This is the item descriptionThis is the item description10. This is the item description";
            FeaturedItems.Add(displayItem);

            List<Item> SuppliesItems = new List<Item>();
            List<Item> DormFurnitureItems = new List<Item>();
            List<Item> ElectronicsItems = new List<Item>();
            List<Item> WomensClothesItems = new List<Item>();
            List<Item> MensClothesItems = new List<Item>();
            List<Item> AccessoriesItems = new List<Item>();
            List<Item> ServicesItems = new List<Item>();
            List<Item> OtherItems = new List<Item>();
            List<Item> BrowseAllItems = new List<Item>();

            ItemsList.FeaturedItems = FeaturedItems;
            ItemsList.SuppliesItems = SuppliesItems;
            ItemsList.DormFurnitureItems = DormFurnitureItems;
            ItemsList.ElectronicsItems = ElectronicsItems;
            ItemsList.WomensClothesItems = WomensClothesItems;
            ItemsList.MensClothesItems = MensClothesItems;
            ItemsList.AccessoriesItems = AccessoriesItems;
            ItemsList.ServicesItems = ServicesItems;
            ItemsList.OtherItems = OtherItems;
            ItemsList.BrowseAllItems = BrowseAllItems;

            return View(ItemsList);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
