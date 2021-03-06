using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class OrderPlural {
        
        [Key()]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        
        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }
        
        [Display(Name = "Employee ID")]
        public System.Int32? EmployeeID { get; set; }
        
        [Display(Name = "Order date")]
        public System.DateTime? OrderDate { get; set; }
        
        [Display(Name = "Required date")]
        public System.DateTime? RequiredDate { get; set; }
        
        [Display(Name = "Shipped date")]
        public System.DateTime? ShippedDate { get; set; }
        
        [Display(Name = "Ship via")]
        public System.Int32? ShipVia { get; set; }
        
        [Display(Name = "Freight")]
        public System.Decimal? Freight { get; set; }
        
        [Display(Name = "Ship name")]
        public string ShipName { get; set; }
        
        [Display(Name = "Ship address")]
        public string ShipAddress { get; set; }
        
        [Display(Name = "Ship city")]
        public string ShipCity { get; set; }
        
        [Display(Name = "Ship region")]
        public string ShipRegion { get; set; }
        
        [Display(Name = "Ship postal code")]
        public string ShipPostalCode { get; set; }
        
        [Display(Name = "Ship country")]
        public string ShipCountry { get; set; }
        
        /// Child OrderDetailPlurals where [Order Details].[OrderID] point to this entity (FK_Order_Details_Orders)
        public virtual ICollection<OrderDetailPlural> OrderDetailPlurals { get; set; } = new HashSet<OrderDetailPlural>();
        
        // Parent Customer pointed by [Orders].([CustomerID]) (FK_Orders_Customers)
        public virtual CustomerPlural Customer { get; set; }
        
        // Parent Employee pointed by [Orders].([EmployeeID]) (FK_Orders_Employees)
        public virtual EmployeePlural Employee { get; set; }
        
        // Parent Shipper pointed by [Orders].([ShipVia]) (FK_Orders_Shippers)
        public virtual ShipperPlural Shipper { get; set; }
    }
}

