using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class EmployeeTerritoryPluralModel {
        
        protected internal int _employeeID;
        
        protected internal string _territoryID;
        
        public EmployeeTerritoryPluralModel() {
        }
        
        internal EmployeeTerritoryPluralModel(EmployeeTerritoryPlural entity) {
            this._employeeID = entity.EmployeeID;
            this._territoryID = entity.TerritoryID;
        }
        
        [Required()]
        [Display(Name = "Employee ID")]
        public int EmployeeID {
            get {
                return this._employeeID;
            }
            set {
                this._employeeID = value;
            }
        }
        
        [Required()]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Territory ID")]
        public string TerritoryID {
            get {
                return this._territoryID;
            }
            set {
                this._territoryID = value;
            }
        }
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=EmployeeID.GetHashCode();
            hash ^=TerritoryID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return EmployeeID.ToString()
                 + "-" + TerritoryID
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmployeeTerritoryPluralModel) {
                EmployeeTerritoryPluralModel toCompare = (EmployeeTerritoryPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmployeeTerritoryPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.EmployeeID == EmployeeID
             && string.Compare(toCompare.TerritoryID, TerritoryID, true) == 0
;
            }

            return result;
        }
    }
}

