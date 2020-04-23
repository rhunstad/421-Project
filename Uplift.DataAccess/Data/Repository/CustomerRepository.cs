using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCustomerListForDropDown()
        {
            return _db.Customer.Select(i => new SelectListItem()
            {
                Text = i.Username,
                Value = i.Email.ToString()
            }); ;
        }

        public void Update(Customer customer)
        {
            var objFromDb = _db.Customer.FirstOrDefault(s => s.Email == customer.Email);

            objFromDb.Username = customer.Username;

            _db.SaveChanges();
        }
    }
}
