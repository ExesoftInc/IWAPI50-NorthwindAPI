using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class EmployeePlural {
        
        [Key()]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "Title of courtesy")]
        public string TitleOfCourtesy { get; set; }
        
        [Display(Name = "Birth date")]
        public System.DateTime? BirthDate { get; set; }
        
        [Display(Name = "Hire date")]
        public System.DateTime? HireDate { get; set; }
        
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
        
        [Display(Name = "Home phone")]
        public string HomePhone { get; set; }
        
        [Display(Name = "Extension")]
        public string Extension { get; set; }
        
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
        
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        
        [Display(Name = "Reports to")]
        public System.Int32? ReportsTo { get; set; }
        
        [Display(Name = "Photo path")]
        public string PhotoPath { get; set; }
        
        /// Child EmployeePlurals where [Employees].[ReportsTo] point to this entity (FK_Employees_Employees)
        public virtual ICollection<EmployeePlural> EmployeePlurals { get; set; } = new HashSet<EmployeePlural>();
        
        /// Child EmployeeTerritoryPlurals where [EmployeeTerritories].[EmployeeID] point to this entity (FK_EmployeeTerritories_Employees)
        public virtual ICollection<EmployeeTerritoryPlural> EmployeeTerritoryPlurals { get; set; } = new HashSet<EmployeeTerritoryPlural>();
        
        /// Child OrderPlurals where [Orders].[EmployeeID] point to this entity (FK_Orders_Employees)
        public virtual ICollection<OrderPlural> OrderPlurals { get; set; } = new HashSet<OrderPlural>();
        
        // Parent Employee pointed by [Employees].([ReportsTo]) (FK_Employees_Employees)
        public virtual EmployeePlural Employee { get; set; }
    }
}

