using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.DataAccess.Data.Repository;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Admin)]
    public class AdminUserController : Controller
    {
        private readonly ILogger<AdminUserController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AdminUserController(ILogger<AdminUserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            var ItemsList = _unitOfWork.Item.GetAll();

            double zero = 0;
            List<double> blank = new List<double>();
            blank.Add(zero);
            blank.Add(zero);
            blank.Add(zero);

            List<double> Supplies = new List<double>();
            Supplies.Add(0);
            Supplies.Add(0);
            Supplies.Add(0);
            List<double> DormFurniture = new List<double>();
            DormFurniture.Add(0);
            DormFurniture.Add(0);
            DormFurniture.Add(0);
            List<double> Electronics = new List<double>();
            Electronics.Add(0);
            Electronics.Add(0);
            Electronics.Add(0);
            List<double> MensClothes = new List<double>();
            MensClothes.Add(0);
            MensClothes.Add(0);
            MensClothes.Add(0);
            List<double> WomensClothes = new List<double>();
            WomensClothes.Add(0);
            WomensClothes.Add(0);
            WomensClothes.Add(0);
            List<double> Accessories = new List<double>();
            Accessories.Add(0);
            Accessories.Add(0);
            Accessories.Add(0);
            List<double> Services = new List<double>();
            Services.Add(0);
            Services.Add(0);
            Services.Add(0);
            List<double> Total = new List<double>();
            Total.Add(0);
            Total.Add(0);
            Total.Add(0);


            List<List<double>> Categories = new List<List<double>>();

            Categories.Add(Supplies);
            Categories.Add(DormFurniture);
            Categories.Add(Electronics);
            Categories.Add(MensClothes);
            Categories.Add(WomensClothes);
            Categories.Add(Accessories);
            Categories.Add(Services);
            Categories.Add(Total);



            foreach (var Item in ItemsList)
            {
                if (Item.ItemCategory == "Supplies")
                {
                    Categories[0][0] += 1;
                    Categories[0][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Dorm Furniture")
                {
                    Categories[1][0] += 1;
                    Categories[1][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Electronics")
                {
                    Categories[2][0] += 1;
                    Categories[2][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Clothes-Men")
                {
                    Categories[3][0] += 1;
                    Categories[3][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Clothes-Women")
                {
                    Categories[4][0] += 1;
                    Categories[4][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Clothes-Accessories")
                {
                    Categories[5][0] += 1;
                    Categories[5][1] += Item.Price;
                }
                else if (Item.ItemCategory == "Services")
                {
                    Categories[6][0] += 1;
                    Categories[6][1] += Item.Price;
                }
                Categories[7][0] += 1;
                Categories[7][1] += Item.Price;
            }

            for (int j = 0; j < Categories.Count(); j++)
            {
                var average = Categories[j][1] / Categories[j][0];
                average = Math.Round(average);
                Categories[j][2] = average;
            }

            return View(Categories);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

