using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class EmployeeTerritoryPlural {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Territory ID")]
        public string TerritoryID { get; set; }
        
        // Parent Employee pointed by [EmployeeTerritories].([EmployeeID]) (FK_EmployeeTerritories_Employees)
        public virtual EmployeePlural Employee { get; set; }
        
        // Parent Territory pointed by [EmployeeTerritories].([TerritoryID]) (FK_EmployeeTerritories_Territories)
        public virtual TerritoryPlural Territory { get; set; }
    }
}

