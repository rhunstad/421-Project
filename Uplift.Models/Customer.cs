using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
     public class Customer
    {

        [Key]
        [Required]
        public string Email { get; set; }

       [Required]
       public string Fname { get; set; }

        [Required]
        public string LName { get; set; }

        public int phoneNumber { get; set; }


        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        
  
    }
}
