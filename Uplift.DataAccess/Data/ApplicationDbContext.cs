using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Item> Item { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; } 

        // Note 4/15/2020 (Ryland): This may cause issues with the built-in keyword "Users", maybe use "Customers" instead?
        //public DbSet<Users> Users {get; set;}
    }
}
