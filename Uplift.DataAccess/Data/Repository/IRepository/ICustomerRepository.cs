using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;
using Uplift.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<SelectListItem> GetCustomerListForDropDown();

        void Update(Customer customer);
    }
}
