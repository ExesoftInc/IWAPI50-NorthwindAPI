using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class SupplierPlural {
        
        [Key()]
        [Display(Name = "Supplier ID")]
        public int SupplierID { get; set; }
        
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
        
        [Display(Name = "Home page")]
        public string HomePage { get; set; }
        
        /// Child ProductPlurals where [Products].[SupplierID] point to this entity (FK_Products_Suppliers)
        public virtual ICollection<ProductPlural> ProductPlurals { get; set; } = new HashSet<ProductPlural>();
    }
}

