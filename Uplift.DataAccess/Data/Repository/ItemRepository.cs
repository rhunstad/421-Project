using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ItemRepository : Repository<Item> ,  IItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetItemListForDropDown()
        {
            return _db.Item.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.ItemID.ToString()
            });;
        }

        public void Update(Item item)
        {
            var objFromDb = _db.Item.FirstOrDefault(s => s.ItemID == item.ItemID);

            objFromDb.Title = item.Title;
            objFromDb.ItemDescription = item.ItemDescription;

            _db.SaveChanges();
        }
    }
}
