using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;
using Uplift.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<SelectListItem> GetOfferListForDropDown();

        void Update(Offer offer);
    }
}
