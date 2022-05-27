using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class OrderDetailPluralModel {
        
        protected internal short _quantity;
        
        protected internal float _discount;
        
        protected internal int _orderID;
        
        protected internal int _productID;
        
        protected internal decimal _unitPrice;
        
        public OrderDetailPluralModel() {
        }
        
        internal OrderDetailPluralModel(OrderDetailPlural entity) {
            this._quantity = entity.Quantity;
            this._discount = entity.Discount;
            this._orderID = entity.OrderID;
            this._productID = entity.ProductID;
            this._unitPrice = entity.UnitPrice;
        }
        
        [Required()]
        [Display(Name = "Quantity")]
        public short Quantity {
            get {
                return this._quantity;
            }
            set {
                this._quantity = value;
            }
        }
        
        [Required()]
        [Display(Name = "Discount")]
        public float Discount {
            get {
                return this._discount;
            }
            set {
                this._discount = value;
            }
        }
        
        [Required()]
        [Display(Name = "Order ID")]
        public int OrderID {
            get {
                return this._orderID;
            }
            set {
                this._orderID = value;
            }
        }
        
        [Required()]
        [Display(Name = "Product ID")]
        public int ProductID {
            get {
                return this._productID;
            }
            set {
                this._productID = value;
            }
        }
        
        [Required()]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit price")]
        public decimal UnitPrice {
            get {
                return this._unitPrice;
            }
            set {
                this._unitPrice = value;
            }
        }
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=OrderID.GetHashCode();
            hash ^=ProductID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return OrderID.ToString()
                 + "-" + ProductID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is OrderDetailPluralModel) {
                OrderDetailPluralModel toCompare = (OrderDetailPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(OrderDetailPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.OrderID == OrderID
             && toCompare.ProductID == ProductID
;
            }

            return result;
        }
    }
}

