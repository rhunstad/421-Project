using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
     public class Item
    {

        [Key]
        [Required]
        
        public Guid ItemID { get; set; }

        [Required]
        public string Title { get; set; }

        
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string ItemDescription { get; set; }

        public DateTime DateSold { get; set; }

        public int ItemsSold { get; set; }

        public string ItemCategory { get; set; }

        
        public string Email { get; set; }

        [Required]
        public Guid SellerID { get; set; }


    }
}
