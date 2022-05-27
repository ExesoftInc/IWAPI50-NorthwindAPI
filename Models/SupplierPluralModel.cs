using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class SupplierPluralModel {
        
        protected internal int _supplierID;
        
        protected internal string _companyName;
        
        protected internal string _contactName;
        
        protected internal string _contactTitle;
        
        protected internal string _address;
        
        protected internal string _city;
        
        protected internal string _region;
        
        protected internal string _postalCode;
        
        protected internal string _country;
        
        protected internal string _phone;
        
        protected internal string _fax;
        
        protected internal string _homePage;
        
        public SupplierPluralModel() {
        }
        
        internal SupplierPluralModel(SupplierPlural entity) {
            this._supplierID = entity.SupplierID;
            this._companyName = entity.CompanyName;
            this._contactName = entity.ContactName;
            this._contactTitle = entity.ContactTitle;
            this._address = entity.Address;
            this._city = entity.City;
            this._region = entity.Region;
            this._postalCode = entity.PostalCode;
            this._country = entity.Country;
            this._phone = entity.Phone;
            this._fax = entity.Fax;
            this._homePage = entity.HomePage;
        }
        
        [Display(Name = "Supplier ID")]
        public int SupplierID {
            get {
                return this._supplierID;
            }
            set {
                this._supplierID = value;
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
        
        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Contact name")]
        public string ContactName {
            get {
                return this._contactName;
            }
            set {
                this._contactName = value;
            }
        }
        
        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Contact title")]
        public string ContactTitle {
            get {
                return this._contactTitle;
            }
            set {
                this._contactTitle = value;
            }
        }
        
        [MaxLength(60)]
        [StringLength(60)]
        [Display(Name = "Address")]
        public string Address {
            get {
                return this._address;
            }
            set {
                this._address = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "City")]
        public string City {
            get {
                return this._city;
            }
            set {
                this._city = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Region")]
        public string Region {
            get {
                return this._region;
            }
            set {
                this._region = value;
            }
        }
        
        [MaxLength(10)]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal code")]
        public string PostalCode {
            get {
                return this._postalCode;
            }
            set {
                this._postalCode = value;
            }
        }
        
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Country")]
        public string Country {
            get {
                return this._country;
            }
            set {
                this._country = value;
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
        
        [MaxLength(24)]
        [StringLength(24)]
        [Phone()]
        [Display(Name = "Fax")]
        public string Fax {
            get {
                return this._fax;
            }
            set {
                this._fax = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Home page")]
        public string HomePage {
            get {
                return this._homePage;
            }
            set {
                this._homePage = value;
            }
        }
        
        /// Child ProductPlurals where [Products].[SupplierID] point to this entity (FK_Products_Suppliers)
        public virtual ICollection<ProductPluralModel> ProductPluralsModel { get; set; } = new HashSet<ProductPluralModel>();
        
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

            if (obj is SupplierPluralModel) {
                SupplierPluralModel toCompare = (SupplierPluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(SupplierPluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CompanyName, CompanyName, true) == 0
             && toCompare.ContactName.Equals(ContactName)
             && toCompare.ContactTitle.Equals(ContactTitle)
             && toCompare.Address.Equals(Address)
             && toCompare.City.Equals(City)
             && toCompare.Region.Equals(Region)
             && toCompare.PostalCode.Equals(PostalCode)
             && toCompare.Country.Equals(Country)
             && toCompare.Phone.Equals(Phone)
             && toCompare.Fax.Equals(Fax)
             && toCompare.HomePage.Equals(HomePage)
;
            }

            return result;
        }
    }
}

