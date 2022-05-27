using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class ProductPlural {
        
        [Key()]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        
        [Display(Name = "Product name")]
        public string ProductName { get; set; }
        
        [Display(Name = "Supplier ID")]
        public System.Int32? SupplierID { get; set; }
        
        [Display(Name = "Category ID")]
        public System.Int32? CategoryID { get; set; }
        
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }
        
        [Display(Name = "Unit price")]
        public System.Decimal? UnitPrice { get; set; }
        
        [Display(Name = "Units in stock")]
        public System.Int16? UnitsInStock { get; set; }
        
        [Display(Name = "Units on order")]
        public System.Int16? UnitsOnOrder { get; set; }
        
        [Display(Name = "Reorder level")]
        public System.Int16? ReorderLevel { get; set; }
        
        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }
        
        /// Child OrderDetailPlurals where [Order Details].[ProductID] point to this entity (FK_Order_Details_Products)
        public virtual ICollection<OrderDetailPlural> OrderDetailPlurals { get; set; } = new HashSet<OrderDetailPlural>();
        
        // Parent Supplier pointed by [Products].([SupplierID]) (FK_Products_Suppliers)
        public virtual SupplierPlural Supplier { get; set; }
        
        // Parent Category pointed by [Products].([CategoryID]) (FK_Products_Categories)
        public virtual CategoryPlural Category { get; set; }
    }
}

