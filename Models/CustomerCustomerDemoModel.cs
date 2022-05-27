using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class CustomerCustomerDemoModel {
        
        protected internal string _customerID;
        
        protected internal string _customerTypeID;
        
        public CustomerCustomerDemoModel() {
        }
        
        internal CustomerCustomerDemoModel(CustomerCustomerDemo entity) {
            this._customerID = entity.CustomerID;
            this._customerTypeID = entity.CustomerTypeID;
        }
        
        [Required()]
        [MaxLength(5)]
        [StringLength(5)]
        [Display(Name = "Customer ID")]
        public string CustomerID {
            get {
                return this._customerID;
            }
            set {
                this._customerID = value;
            }
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
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CustomerID.GetHashCode();
            hash ^=CustomerTypeID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return CustomerID
                 + "-" + CustomerTypeID
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is CustomerCustomerDemoModel) {
                CustomerCustomerDemoModel toCompare = (CustomerCustomerDemoModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(CustomerCustomerDemoModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CustomerID, CustomerID, true) == 0
             && string.Compare(toCompare.CustomerTypeID, CustomerTypeID, true) == 0
;
            }

            return result;
        }
    }
}

