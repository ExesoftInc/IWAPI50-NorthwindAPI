using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class TerritoryPlural {
        
        [Key()]
        [Display(Name = "Territory ID")]
        public string TerritoryID { get; set; }
        
        [Display(Name = "Territory description")]
        public string TerritoryDescription { get; set; }
        
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }
        
        /// Child EmployeeTerritoryPlurals where [EmployeeTerritories].[TerritoryID] point to this entity (FK_EmployeeTerritories_Territories)
        public virtual ICollection<EmployeeTerritoryPlural> EmployeeTerritoryPlurals { get; set; } = new HashSet<EmployeeTerritoryPlural>();
        
        // Parent Region pointed by [Territories].([RegionID]) (FK_Territories_Region)
        public virtual Region Region { get; set; }
    }
}

