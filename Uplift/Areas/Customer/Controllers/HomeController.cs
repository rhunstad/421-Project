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
using Uplift.DataAccess.Data.Repository;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            List<Item> FeaturedItems = new List<Item>();
            List<Item> SuppliesItems = new List<Item>();
            List<Item> DormFurnitureItems = new List<Item>();
            List<Item> ElectronicsItems = new List<Item>();
            List<Item> WomensClothesItems = new List<Item>();
            List<Item> MensClothesItems = new List<Item>();
            List<Item> AccessoriesItems = new List<Item>();
            List<Item> ServicesItems = new List<Item>();
            List<Item> OtherItems = new List<Item>();
            List<Item> BrowseAllItems = new List<Item>();



            var ItemsList = _unitOfWork.Item.GetAll();
            dynamic ViewModel = new ExpandoObject();
            var count = 0;
            var featured1 = 3;
            var featured2 = 5;
            var featured3 = 7;
            var featured4 = 1;

            foreach (var Item in ItemsList)
            {
                if ((Item.ItemCategory == "Supplies") & (SuppliesItems.Count < 4))
                {
                    SuppliesItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Dorm Furniture") & (DormFurnitureItems.Count < 4))
                {
                    DormFurnitureItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Electronics") & (ElectronicsItems.Count < 4))
                {
                    ElectronicsItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Clothes-Men") &(MensClothesItems.Count < 4))
                {
                    MensClothesItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Clothes-Women") & (WomensClothesItems.Count < 4))
                {
                    WomensClothesItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Clothes-Accessories") & (AccessoriesItems.Count < 4))
                {
                    AccessoriesItems.Add(Item);
                }
                else if ((Item.ItemCategory == "Services") & (ServicesItems.Count < 4))
                {
                    ServicesItems.Add(Item);
                }
                if ((count == featured1) | (count == featured2) | (count == featured3) | (count == featured4))
                {
                    FeaturedItems.Add(Item);
                }
                if ((count == 0) | (count == 1) | (count == 2) | (count == 3))
                {
                    BrowseAllItems.Add(Item);
                }
                count += 1;
            }


            ViewModel.FeaturedItems = FeaturedItems;
            ViewModel.SuppliesItems = SuppliesItems;
            ViewModel.DormFurnitureItems = DormFurnitureItems;
            ViewModel.ElectronicsItems = ElectronicsItems;
            ViewModel.WomensClothesItems = WomensClothesItems;
            ViewModel.MensClothesItems = MensClothesItems;
            ViewModel.AccessoriesItems = AccessoriesItems;
            ViewModel.ServicesItems = ServicesItems;
            ViewModel.OtherItems = OtherItems;
            ViewModel.BrowseAllItems = BrowseAllItems;

            if (ViewModel == null)
            {
                return NotFound();
            }

            return View(ViewModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
