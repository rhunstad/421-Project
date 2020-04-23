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
       public string FName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string BuyerName { get; set; }

        [Required]
        public DateTime OfferDate { get; set; }

       

        
  
    }
}
