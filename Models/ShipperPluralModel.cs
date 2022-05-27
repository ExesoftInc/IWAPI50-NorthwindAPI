using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class ShipperPluralModel {
        
        protected internal int _shipperID;
        
        protected internal string _companyName;
        
        protected internal string _phone;
        
        public ShipperPluralModel() {
        }
        
        internal ShipperPluralModel(ShipperPlural entity) {
            this._shipperID = entity.ShipperID;
            this._companyName = entity.CompanyName;
            this._phone = entity.Phone;
        }
        
        [Display(Name = "Shipper ID")]
        public int ShipperID {
            get {
                return this._shipperID;
            }
            set {
                this._shipperID = value;
            }
        }
        
        [Required()]
        [MaxLength(40)]
        [StringLength(40)]
        [Display(Name = "Company name")]
        public string CompanyName {
            get {
                return this._companyName;
            }
            set {
                this._companyName = value;
            }
        }
        
        [MaxLength(24)]
        [StringLength(24)]
        [Phone()]
        [Display(Name = "Phone")]
        public string Phone {
            get {
                return this._phone;
            }
            set {
                this._phone = value;
            }
        }
        
        /// Child OrderPlurals where [Orders].[ShipVia] point to this entity (FK_Orders_Shippers)
        public virtual ICollection<OrderPluralModel> OrderPluralsModel { get; set; } = new HashSet<OrderPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CompanyName.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return CompanyName
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is ShipperPluralModel) {
                ShipperPluralModel toCompare = (ShipperPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(ShipperPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CompanyName, CompanyName, true) == 0
             && toCompare.Phone.Equals(Phone)
;
            }

            return result;
        }
    }
}

