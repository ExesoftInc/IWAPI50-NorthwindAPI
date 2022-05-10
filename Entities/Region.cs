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
    
    
    public partial class Region {
        
        [Key()]
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }
        
        [Display(Name = "Region description")]
        public string RegionDescription { get; set; }
        
        /// Child TerritoryPlurals where [Territories].[RegionID] point to this entity (FK_Territories_Region)
        public virtual ICollection<TerritoryPlural> TerritoryPlurals { get; set; } = new HashSet<TerritoryPlural>();
    }
}

