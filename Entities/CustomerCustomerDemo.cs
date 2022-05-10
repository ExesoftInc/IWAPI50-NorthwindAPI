// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class CustomerCustomerDemo {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Customer type ID")]
        public string CustomerTypeID { get; set; }
        
        // Parent Customer pointed by [CustomerCustomerDemo].([CustomerID]) (FK_CustomerCustomerDemo_Customers)
        public virtual CustomerPlural Customer { get; set; }
        
        // Parent CustomerDemographic pointed by [CustomerCustomerDemo].([CustomerTypeID]) (FK_CustomerCustomerDemo)
        public virtual CustomerDemographicPlural CustomerDemographic { get; set; }
    }
}

