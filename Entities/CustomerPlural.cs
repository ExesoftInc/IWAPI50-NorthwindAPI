using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class CustomerPlural {
        
        [Key()]
        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }
        
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        
        [Display(Name = "Contact name")]
        public string ContactName { get; set; }
        
        [Display(Name = "Contact title")]
        public string ContactTitle { get; set; }
        
        [Display(Name = "Address")]
        public string Address { get; set; }
        
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Display(Name = "Region")]
        public string Region { get; set; }
        
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        
        /// Child CustomerCustomerDemoes where [CustomerCustomerDemo].[CustomerID] point to this entity (FK_CustomerCustomerDemo_Customers)
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; } = new HashSet<CustomerCustomerDemo>();
        
        /// Child OrderPlurals where [Orders].[CustomerID] point to this entity (FK_Orders_Customers)
        public virtual ICollection<OrderPlural> OrderPlurals { get; set; } = new HashSet<OrderPlural>();
    }
}

