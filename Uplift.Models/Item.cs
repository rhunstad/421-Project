using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace Uplift.Models
{

    public class Item
    {
        
        [Key]
        [Required]
        [JsonIgnore]
        [FieldBuilderIgnore]
        public Guid ItemID { get; set; }

        [Required]
        [IsSearchable, IsFilterable, IsSortable]
        public string Title { get; set; }

        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [IsSearchable, IsFilterable, IsSortable]
        public double Price { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string ItemDescription { get; set; }

        public DateTime DateSold { get; set; }

        public int ItemsSold { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string ItemCategory { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        [FieldBuilderIgnore]
        public Guid SellerID { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Upload)]
        public byte[] ItemImage { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
