using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class CategoryPlural {
        
        [Key()]
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        
        /// Child ProductPlurals where [Products].[CategoryID] point to this entity (FK_Products_Categories)
        public virtual ICollection<ProductPlural> ProductPlurals { get; set; } = new HashSet<ProductPlural>();
    }
}

