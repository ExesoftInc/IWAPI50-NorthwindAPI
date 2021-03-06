using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class OrderPluralModel {
        
        protected internal int _orderID;
        
        protected internal string _customerID;
        
        protected internal int? _employeeID;
        
        protected internal System.DateTime? _orderDate;
        
        protected internal System.DateTime? _requiredDate;
        
        protected internal System.DateTime? _shippedDate;
        
        protected internal int? _shipVia;
        
        protected internal decimal? _freight;
        
        protected internal string _shipName;
        
        protected internal string _shipAddress;
        
        protected internal string _shipCity;
        
        protected internal string _shipRegion;
        
        protected internal string _shipPostalCode;
        
        protected internal string _shipCountry;
        
        public OrderPluralModel() {
        }
        
        internal OrderPluralModel(OrderPlural entity) {
            this._orderID = entity.OrderID;
            this._customerID = entity.CustomerID;
            this._employeeID = entity.EmployeeID;
            this._orderDate = entity.OrderDate;
            this._requiredDate = entity.RequiredDate;
            this._shippedDate = entity.ShippedDate;
            this._shipVia = entity.ShipVia;
            this._freight = entity.Freight;
            this._shipName = entity.ShipName;
            this._shipAddress = entity.ShipAddress;
            this._shipCity = entity.ShipCity;
            this._shipRegion = entity.ShipRegion;
            this._shipPostalCode = entity.ShipPostalCode;
            this._shipCountry = entity.ShipCountry;
        }
        
        [Display(Name = "Order ID")]
        public int OrderID {
            get {
                return this._orderID;
            }
            set {
                this._orderID = value;
            }
        }
        
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
        
        [Display(Name = "Employee ID")]
        public int? EmployeeID {
            get {
                return this._employeeID;
            }
            set {
                this._employeeID = value;
            }
        }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Order date")]
        public System.DateTime? OrderDate {
            get {
                return this._orderDate;
            }
            set {
                this._orderDate = value;
            }
        }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Required date")]
        public System.DateTime? RequiredDate {
            get {
                return this._requiredDate;
            }
            set {
                this._requiredDate = value;
            }
        }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Shipped date")]
        public System.DateTime? ShippedDate {
            get {
                return this._shippedDate;
            }
            set {
                this._shippedDate = value;
            }
        }
        
        [Display(Name = "Ship via")]
        public int? ShipVia {
            get {
                return this._shipVia;
            }
            set {
                this._shipVia = value;
            }
        }
        
        [DataType(DataType.Currency)]
        [Display(Name = "Freight")]
        public decimal? Freight {
            get {
                return this._freight;
            }
            set {
                this._freight = value;
            }
        }
        
        [MaxLength(40)]
        [StringLength(40)]
        [Display(Name = "Ship name")]
        public string ShipName {
            get {
                return this._shipName;
            }
            set {
                this._shipName = value;
            }
        }
        
        [MaxLength(60)]
        [StringLength(60)]
        [Display(Name = "Ship address")]
        public string ShipAddress {
            get {
                return this._shipAddress;
            }
            set {
                this._shipAddress = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Ship city")]
        public string ShipCity {
            get {
                return this._shipCity;
            }
            set {
                this._shipCity = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Ship region")]
        public string ShipRegion {
            get {
                return this._shipRegion;
            }
            set {
                this._shipRegion = value;
            }
        }
        
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Ship postal code")]
        public string ShipPostalCode {
            get {
                return this._shipPostalCode;
            }
            set {
                this._shipPostalCode = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Ship country")]
        public string ShipCountry {
            get {
                return this._shipCountry;
            }
            set {
                this._shipCountry = value;
            }
        }
        
        /// Child OrderDetailPlurals where [Order Details].[OrderID] point to this entity (FK_Order_Details_Orders)
        public virtual ICollection<OrderDetailPluralModel> OrderDetailPluralsModel { get; set; } = new HashSet<OrderDetailPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CustomerID.GetHashCode();
            hash ^=EmployeeID.GetHashCode();
            hash ^=OrderDate.GetHashCode();
            hash ^=RequiredDate.GetHashCode();
            hash ^=ShippedDate.GetHashCode();
            hash ^=ShipVia.GetHashCode();
            hash ^=Freight.GetHashCode();
            hash ^=ShipName.GetHashCode();
            hash ^=ShipAddress.GetHashCode();
            hash ^=ShipCity.GetHashCode();
            hash ^=ShipRegion.GetHashCode();
            hash ^=ShipPostalCode.GetHashCode();
            hash ^=ShipCountry.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return GetHashCode().ToString();
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is OrderPluralModel) {
                OrderPluralModel toCompare = (OrderPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(OrderPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.CustomerID.Equals(CustomerID)
             && toCompare.EmployeeID.Equals(EmployeeID)
             && toCompare.OrderDate.Equals(OrderDate)
             && toCompare.RequiredDate.Equals(RequiredDate)
             && toCompare.ShippedDate.Equals(ShippedDate)
             && toCompare.ShipVia.Equals(ShipVia)
             && toCompare.Freight.Equals(Freight)
             && toCompare.ShipName.Equals(ShipName)
             && toCompare.ShipAddress.Equals(ShipAddress)
             && toCompare.ShipCity.Equals(ShipCity)
             && toCompare.ShipRegion.Equals(ShipRegion)
             && toCompare.ShipPostalCode.Equals(ShipPostalCode)
             && toCompare.ShipCountry.Equals(ShipCountry)
;
            }

            return result;
        }
    }
}

