using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class CategoryPluralModel {
        
        protected internal int _categoryID;
        
        protected internal string _categoryName;
        
        protected internal string _description;
        
        protected internal byte[] _picture;
        
        public CategoryPluralModel() {
        }
        
        internal CategoryPluralModel(CategoryPlural entity) {
            this._categoryID = entity.CategoryID;
            this._categoryName = entity.CategoryName;
            this._description = entity.Description;
            this._picture = entity.Picture;
        }
        
        [Display(Name = "Category ID")]
        public int CategoryID {
            get {
                return this._categoryID;
            }
            set {
                this._categoryID = value;
            }
        }
        
        [Required()]
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Category name")]
        public string CategoryName {
            get {
                return this._categoryName;
            }
            set {
                this._categoryName = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Description")]
        public string Description {
            get {
                return this._description;
            }
            set {
                this._description = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Picture")]
        public byte[] Picture {
            get {
                return this._picture;
            }
            set {
                this._picture = value;
            }
        }
        
        /// Child ProductPlurals where [Products].[CategoryID] point to this entity (FK_Products_Categories)
        public virtual ICollection<ProductPluralModel> ProductPluralsModel { get; set; } = new HashSet<ProductPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CategoryName.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return CategoryName
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is CategoryPluralModel) {
                CategoryPluralModel toCompare = (CategoryPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(CategoryPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CategoryName, CategoryName, true) == 0
             && toCompare.Description.Equals(Description)
             && toCompare.Picture.Equals(Picture)
;
            }

            return result;
        }
    }
}

