using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private readonly ApplicationDbContext _db;

        public OfferRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetOfferListForDropDown()
        {
            return _db.Offer.Select(i => new SelectListItem()
            {
                Value = i.ItemID.ToString()
            }); ;
        }

        public void Update(Offer offer)
        {
            var objFromDb = _db.Offer.FirstOrDefault(s => s.ItemID == offer.ItemID);

            objFromDb.FName = offer.FName;
            objFromDb.LName = offer.LName;

            _db.SaveChanges();
        }
    }
}
