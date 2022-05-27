using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class ShipperPlural {
        
        [Key()]
        [Display(Name = "Shipper ID")]
        public int ShipperID { get; set; }
        
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        
        /// Child OrderPlurals where [Orders].[ShipVia] point to this entity (FK_Orders_Shippers)
        public virtual ICollection<OrderPlural> OrderPlurals { get; set; } = new HashSet<OrderPlural>();
    }
}

