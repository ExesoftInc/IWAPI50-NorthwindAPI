using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models {
    
    
    public class EmployeePluralModel {
        
        protected internal int _employeeID;
        
        protected internal string _lastName;
        
        protected internal string _firstName;
        
        protected internal string _title;
        
        protected internal string _titleOfCourtesy;
        
        protected internal System.DateTime? _birthDate;
        
        protected internal System.DateTime? _hireDate;
        
        protected internal string _address;
        
        protected internal string _city;
        
        protected internal string _region;
        
        protected internal string _postalCode;
        
        protected internal string _country;
        
        protected internal string _homePhone;
        
        protected internal string _extension;
        
        protected internal byte[] _photo;
        
        protected internal string _notes;
        
        protected internal int? _reportsTo;
        
        protected internal string _photoPath;
        
        public EmployeePluralModel() {
        }
        
        internal EmployeePluralModel(EmployeePlural entity) {
            this._employeeID = entity.EmployeeID;
            this._lastName = entity.LastName;
            this._firstName = entity.FirstName;
            this._title = entity.Title;
            this._titleOfCourtesy = entity.TitleOfCourtesy;
            this._birthDate = entity.BirthDate;
            this._hireDate = entity.HireDate;
            this._address = entity.Address;
            this._city = entity.City;
            this._region = entity.Region;
            this._postalCode = entity.PostalCode;
            this._country = entity.Country;
            this._homePhone = entity.HomePhone;
            this._extension = entity.Extension;
            this._photo = entity.Photo;
            this._notes = entity.Notes;
            this._reportsTo = entity.ReportsTo;
            this._photoPath = entity.PhotoPath;
        }
        
        [Display(Name = "Employee ID")]
        public int EmployeeID {
            get {
                return this._employeeID;
            }
            set {
                this._employeeID = value;
            }
        }
        
        [Required()]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName {
            get {
                return this._lastName;
            }
            set {
                this._lastName = value;
            }
        }
        
        [Required()]
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "First name")]
        public string FirstName {
            get {
                return this._firstName;
            }
            set {
                this._firstName = value;
            }
        }
        
        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Title")]
        public string Title {
            get {
                return this._title;
            }
            set {
                this._title = value;
            }
        }
        
        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Title of courtesy")]
        public string TitleOfCourtesy {
            get {
                return this._titleOfCourtesy;
            }
            set {
                this._titleOfCourtesy = value;
            }
        }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Birth date")]
        public System.DateTime? BirthDate {
            get {
                return this._birthDate;
            }
            set {
                this._birthDate = value;
            }
        }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Hire date")]
        public System.DateTime? HireDate {
            get {
                return this._hireDate;
            }
            set {
                this._hireDate = value;
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
        [Display(Name = "Home phone")]
        public string HomePhone {
            get {
                return this._homePhone;
            }
            set {
                this._homePhone = value;
            }
        }
        
        [MaxLength(4)]
        [StringLength(4)]
        [Display(Name = "Extension")]
        public string Extension {
            get {
                return this._extension;
            }
            set {
                this._extension = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Photo")]
        public byte[] Photo {
            get {
                return this._photo;
            }
            set {
                this._photo = value;
            }
        }
        
        [MaxLength()]
        [Display(Name = "Notes")]
        public string Notes {
            get {
                return this._notes;
            }
            set {
                this._notes = value;
            }
        }
        
        [Display(Name = "Reports to")]
        public int? ReportsTo {
            get {
                return this._reportsTo;
            }
            set {
                this._reportsTo = value;
            }
        }
        
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Photo path")]
        public string PhotoPath {
            get {
                return this._photoPath;
            }
            set {
                this._photoPath = value;
            }
        }
        
        /// Child EmployeePlurals where [Employees].[ReportsTo] point to this entity (FK_Employees_Employees)
        public virtual ICollection<EmployeePluralModel> EmployeePluralsModel { get; set; } = new HashSet<EmployeePluralModel>();
        
        /// Child EmployeeTerritoryPlurals where [EmployeeTerritories].[EmployeeID] point to this entity (FK_EmployeeTerritories_Employees)
        public virtual ICollection<EmployeeTerritoryPluralModel> EmployeeTerritoryPluralsModel { get; set; } = new HashSet<EmployeeTerritoryPluralModel>();
        
        /// Child OrderPlurals where [Orders].[EmployeeID] point to this entity (FK_Orders_Employees)
        public virtual ICollection<OrderPluralModel> OrderPluralsModel { get; set; } = new HashSet<OrderPluralModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=LastName.GetHashCode();
            hash ^=FirstName.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return LastName
                 + "-" + FirstName
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmployeePluralModel) {
                EmployeePluralModel toCompare = (EmployeePluralModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmployeePluralModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.LastName, LastName, true) == 0
             && string.Compare(toCompare.FirstName, FirstName, true) == 0
             && toCompare.Title.Equals(Title)
             && toCompare.TitleOfCourtesy.Equals(TitleOfCourtesy)
             && toCompare.BirthDate.Equals(BirthDate)
             && toCompare.HireDate.Equals(HireDate)
             && toCompare.Address.Equals(Address)
             && toCompare.City.Equals(City)
             && toCompare.Region.Equals(Region)
             && toCompare.PostalCode.Equals(PostalCode)
             && toCompare.Country.Equals(Country)
             && toCompare.HomePhone.Equals(HomePhone)
             && toCompare.Extension.Equals(Extension)
             && toCompare.Photo.Equals(Photo)
             && toCompare.Notes.Equals(Notes)
             && toCompare.ReportsTo.Equals(ReportsTo)
             && toCompare.PhotoPath.Equals(PhotoPath)
;
            }

            return result;
        }
    }
}

