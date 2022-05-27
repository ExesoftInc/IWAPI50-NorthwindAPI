using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Entities {
    
    
    public partial class CustomerDemographicPlural {
        
        [Key()]
        [Display(Name = "Customer type ID")]
        public string CustomerTypeID { get; set; }
        
        [Display(Name = "Customer desc")]
        public string CustomerDesc { get; set; }
        
        /// Child CustomerCustomerDemoes where [CustomerCustomerDemo].[CustomerTypeID] point to this entity (FK_CustomerCustomerDemo)
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; } = new HashSet<CustomerCustomerDemo>();
    }
}

