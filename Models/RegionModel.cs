using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class RegionModel {
        
        protected internal int _regionID;
        
        protected internal string _regionDescription;
        
        public RegionModel() {
        }
        
        internal RegionModel(Region entity) {
            this._regionID = entity.RegionID;
            this._regionDescription = entity.RegionDescription;
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
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Region description")]
        public string RegionDescription {
            get {
                return this._regionDescription;
            }
            set {
                this._regionDescription = value;
            }
        }
        
        /// Child TerritoryPlurals where [Territories].[RegionID] point to this entity (FK_Territories_Region)
        public virtual ICollection<TerritoryPluralModel> TerritoryPluralsModel { get; set; } = new HashSet<TerritoryPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=RegionID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return RegionID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is RegionModel) {
                RegionModel toCompare = (RegionModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(RegionModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.RegionID == RegionID
;
            }

            return result;
        }
    }
}

