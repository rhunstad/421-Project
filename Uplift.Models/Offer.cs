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
        public string buyerEmail { get; set; }

        [Required]
        public string Email { get; set; }

      [Required]
        public Guid SellerID { get; set; }

       
        public Guid BuyerID { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        [Key]
        [Required]
        public DateTime OfferDate { get; set; }

    }
}
