using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class ProductPluralModel {
        
        protected internal int _productID;
        
        protected internal string _productName;
        
        protected internal int? _supplierID;
        
        protected internal int? _categoryID;
        
        protected internal string _quantityPerUnit;
        
        protected internal decimal? _unitPrice;
        
        protected internal short? _unitsInStock;
        
        protected internal short? _unitsOnOrder;
        
        protected internal short? _reorderLevel;
        
        protected internal bool _discontinued;
        
        public ProductPluralModel() {
        }
        
        internal ProductPluralModel(ProductPlural entity) {
            this._productID = entity.ProductID;
            this._productName = entity.ProductName;
            this._supplierID = entity.SupplierID;
            this._categoryID = entity.CategoryID;
            this._quantityPerUnit = entity.QuantityPerUnit;
            this._unitPrice = entity.UnitPrice;
            this._unitsInStock = entity.UnitsInStock;
            this._unitsOnOrder = entity.UnitsOnOrder;
            this._reorderLevel = entity.ReorderLevel;
            this._discontinued = entity.Discontinued;
        }
        
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
        [MaxLength(40)]
        [StringLength(40)]
        [Display(Name = "Product name")]
        public string ProductName {
            get {
                return this._productName;
            }
            set {
                this._productName = value;
            }
        }
        
        [Display(Name = "Supplier ID")]
        public int? SupplierID {
            get {
                return this._supplierID;
            }
            set {
                this._supplierID = value;
            }
        }
        
        [Display(Name = "Category ID")]
        public int? CategoryID {
            get {
                return this._categoryID;
            }
            set {
                this._categoryID = value;
            }
        }
        
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit {
            get {
                return this._quantityPerUnit;
            }
            set {
                this._quantityPerUnit = value;
            }
        }
        
        [DataType(DataType.Currency)]
        [Display(Name = "Unit price")]
        public decimal? UnitPrice {
            get {
                return this._unitPrice;
            }
            set {
                this._unitPrice = value;
            }
        }
        
        [Display(Name = "Units in stock")]
        public short? UnitsInStock {
            get {
                return this._unitsInStock;
            }
            set {
                this._unitsInStock = value;
            }
        }
        
        [Display(Name = "Units on order")]
        public short? UnitsOnOrder {
            get {
                return this._unitsOnOrder;
            }
            set {
                this._unitsOnOrder = value;
            }
        }
        
        [Display(Name = "Reorder level")]
        public short? ReorderLevel {
            get {
                return this._reorderLevel;
            }
            set {
                this._reorderLevel = value;
            }
        }
        
        [Required()]
        [Display(Name = "Discontinued")]
        public bool Discontinued {
            get {
                return this._discontinued;
            }
            set {
                this._discontinued = value;
            }
        }
        
        /// Child OrderDetailPlurals where [Order Details].[ProductID] point to this entity (FK_Order_Details_Products)
        public virtual ICollection<OrderDetailPluralModel> OrderDetailPluralsModel { get; set; } = new HashSet<OrderDetailPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=ProductName.GetHashCode();
            hash ^=Discontinued.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return ProductName
                 + "-" + Discontinued.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is ProductPluralModel) {
                ProductPluralModel toCompare = (ProductPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(ProductPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.ProductName, ProductName, true) == 0
             && toCompare.SupplierID.Equals(SupplierID)
             && toCompare.CategoryID.Equals(CategoryID)
             && toCompare.QuantityPerUnit.Equals(QuantityPerUnit)
             && toCompare.UnitPrice.Equals(UnitPrice)
             && toCompare.UnitsInStock.Equals(UnitsInStock)
             && toCompare.UnitsOnOrder.Equals(UnitsOnOrder)
             && toCompare.ReorderLevel.Equals(ReorderLevel)
             && toCompare.Discontinued == Discontinued
;
            }

            return result;
        }
    }
}

