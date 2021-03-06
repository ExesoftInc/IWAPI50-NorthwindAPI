using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class CustomerDemographicPluralModel {
        
        protected internal string _customerTypeID;
        
        protected internal string _customerDesc;
        
        public CustomerDemographicPluralModel() {
        }
        
        internal CustomerDemographicPluralModel(CustomerDemographicPlural entity) {
            this._customerTypeID = entity.CustomerTypeID;
            this._customerDesc = entity.CustomerDesc;
        }
        
        [Required()]
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Customer type ID")]
        public string CustomerTypeID {
            get {
                return this._customerTypeID;
            }
            set {
                this._customerTypeID = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Customer desc")]
        public string CustomerDesc {
            get {
                return this._customerDesc;
            }
            set {
                this._customerDesc = value;
            }
        }
        
        /// Child CustomerCustomerDemoes where [CustomerCustomerDemo].[CustomerTypeID] point to this entity (FK_CustomerCustomerDemo)
        public virtual ICollection<CustomerCustomerDemoModel> CustomerCustomerDemoesModel { get; set; } = new HashSet<CustomerCustomerDemoModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CustomerTypeID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return CustomerTypeID
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is CustomerDemographicPluralModel) {
                CustomerDemographicPluralModel toCompare = (CustomerDemographicPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(CustomerDemographicPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CustomerTypeID, CustomerTypeID, true) == 0
;
            }

            return result;
        }
    }
}

