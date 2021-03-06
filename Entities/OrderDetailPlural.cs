using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class OrderDetailPlural {
        
        [Display(Name = "Quantity")]
        public short Quantity { get; set; }
        
        [Display(Name = "Discount")]
        public float Discount { get; set; }
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }
        
        // Parent Order pointed by [Order Details].([OrderID]) (FK_Order_Details_Orders)
        public virtual OrderPlural Order { get; set; }
        
        // Parent Product pointed by [Order Details].([ProductID]) (FK_Order_Details_Products)
        public virtual ProductPlural Product { get; set; }
    }
}

