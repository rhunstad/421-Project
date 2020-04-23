using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
     public class Offer
    {

        
        [Required]
        public Guid ItemID { get; set; }

       [Required]
       public string Fname { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string buyerName { get; set; }

        [Required]
        public DateTime offerDate { get; set; }

       

        
  
    }
}
