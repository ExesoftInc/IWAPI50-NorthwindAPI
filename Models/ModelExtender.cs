// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public static class ModelExtender {
        
        public static CategoryPlural ToEntity(this CategoryPluralModel model, CategoryPlural entity) {

            entity.CategoryName = model.CategoryName;
            entity.Description = model.Description;
            entity.Picture = model.Picture;

            return entity;
        }
        
        public static CustomerCustomerDemo ToEntity(this CustomerCustomerDemoModel model, CustomerCustomerDemo entity) {


            return entity;
        }
        
        public static CustomerDemographicPlural ToEntity(this CustomerDemographicPluralModel model, CustomerDemographicPlural entity) {

            entity.CustomerDesc = model.CustomerDesc;

            return entity;
        }
        
        public static CustomerPlural ToEntity(this CustomerPluralModel model, CustomerPlural entity) {

            entity.CompanyName = model.CompanyName;
            entity.ContactName = model.ContactName;
            entity.ContactTitle = model.ContactTitle;
            entity.Address = model.Address;
            entity.City = model.City;
            entity.Region = model.Region;
            entity.PostalCode = model.PostalCode;
            entity.Country = model.Country;
            entity.Phone = model.Phone;
            entity.Fax = model.Fax;

            return entity;
        }
        
        public static EmployeePlural ToEntity(this EmployeePluralModel model, EmployeePlural entity) {

            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.Title = model.Title;
            entity.TitleOfCourtesy = model.TitleOfCourtesy;
            entity.BirthDate = model.BirthDate;
            entity.HireDate = model.HireDate;
            entity.Address = model.Address;
            entity.City = model.City;
            entity.Region = model.Region;
            entity.PostalCode = model.PostalCode;
            entity.Country = model.Country;
            entity.HomePhone = model.HomePhone;
            entity.Extension = model.Extension;
            entity.Photo = model.Photo;
            entity.Notes = model.Notes;
            entity.ReportsTo = model.ReportsTo;
            entity.PhotoPath = model.PhotoPath;

            return entity;
        }
        
        public static EmployeeTerritoryPlural ToEntity(this EmployeeTerritoryPluralModel model, EmployeeTerritoryPlural entity) {


            return entity;
        }
        
        public static OrderDetailPlural ToEntity(this OrderDetailPluralModel model, OrderDetailPlural entity) {

            entity.Quantity = model.Quantity;
            entity.Discount = model.Discount;
            entity.UnitPrice = model.UnitPrice;

            return entity;
        }
        
        public static OrderPlural ToEntity(this OrderPluralModel model, OrderPlural entity) {

            entity.CustomerID = model.CustomerID;
            entity.EmployeeID = model.EmployeeID;
            entity.OrderDate = model.OrderDate;
            entity.RequiredDate = model.RequiredDate;
            entity.ShippedDate = model.ShippedDate;
            entity.ShipVia = model.ShipVia;
            entity.Freight = model.Freight;
            entity.ShipName = model.ShipName;
            entity.ShipAddress = model.ShipAddress;
            entity.ShipCity = model.ShipCity;
            entity.ShipRegion = model.ShipRegion;
            entity.ShipPostalCode = model.ShipPostalCode;
            entity.ShipCountry = model.ShipCountry;

            return entity;
        }
        
        public static ProductPlural ToEntity(this ProductPluralModel model, ProductPlural entity) {

            entity.ProductName = model.ProductName;
            entity.SupplierID = model.SupplierID;
            entity.CategoryID = model.CategoryID;
            entity.QuantityPerUnit = model.QuantityPerUnit;
            entity.UnitPrice = model.UnitPrice;
            entity.UnitsInStock = model.UnitsInStock;
            entity.UnitsOnOrder = model.UnitsOnOrder;
            entity.ReorderLevel = model.ReorderLevel;
            entity.Discontinued = model.Discontinued;

            return entity;
        }
        
        public static Region ToEntity(this RegionModel model, Region entity) {

            entity.RegionDescription = model.RegionDescription;

            return entity;
        }
        
        public static ShipperPlural ToEntity(this ShipperPluralModel model, ShipperPlural entity) {

            entity.CompanyName = model.CompanyName;
            entity.Phone = model.Phone;

            return entity;
        }
        
        public static SupplierPlural ToEntity(this SupplierPluralModel model, SupplierPlural entity) {

            entity.CompanyName = model.CompanyName;
            entity.ContactName = model.ContactName;
            entity.ContactTitle = model.ContactTitle;
            entity.Address = model.Address;
            entity.City = model.City;
            entity.Region = model.Region;
            entity.PostalCode = model.PostalCode;
            entity.Country = model.Country;
            entity.Phone = model.Phone;
            entity.Fax = model.Fax;
            entity.HomePage = model.HomePage;

            return entity;
        }
        
        public static TerritoryPlural ToEntity(this TerritoryPluralModel model, TerritoryPlural entity) {

            entity.TerritoryDescription = model.TerritoryDescription;
            entity.RegionID = model.RegionID;

            return entity;
        }
    }
}

