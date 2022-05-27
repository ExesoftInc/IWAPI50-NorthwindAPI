using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class TerritoryPluralModel {
        
        protected internal string _territoryID;
        
        protected internal string _territoryDescription;
        
        protected internal int _regionID;
        
        public TerritoryPluralModel() {
        }
        
        internal TerritoryPluralModel(TerritoryPlural entity) {
            this._territoryID = entity.TerritoryID;
            this._territoryDescription = entity.TerritoryDescription;
            this._regionID = entity.RegionID;
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
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Territory description")]
        public string TerritoryDescription {
            get {
                return this._territoryDescription;
            }
            set {
                this._territoryDescription = value;
            }
        }
        
        [Required()]
        [Display(Name = "Region ID")]
        public int RegionID {
            get {
                return this._regionID;
            }
            set {
                this._regionID = value;
            }
        }
        
        /// Child EmployeeTerritoryPlurals where [EmployeeTerritories].[TerritoryID] point to this entity (FK_EmployeeTerritories_Territories)
        public virtual ICollection<EmployeeTerritoryPluralModel> EmployeeTerritoryPluralsModel { get; set; } = new HashSet<EmployeeTerritoryPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=TerritoryID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return TerritoryID
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is TerritoryPluralModel) {
                TerritoryPluralModel toCompare = (TerritoryPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(TerritoryPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.TerritoryID, TerritoryID, true) == 0
;
            }

            return result;
        }
    }
}

