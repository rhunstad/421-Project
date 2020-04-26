using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
     public class Customer
    {

        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string LName { get; set; }

        public long PhoneNumber { get; set; }


        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        [Key]
        [Required]
        public Guid SellerID { get; set; }

        
  
    }
}
