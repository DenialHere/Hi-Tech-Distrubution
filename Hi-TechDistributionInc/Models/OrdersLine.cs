//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hi_TechDistributionInc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdersLine
    {
        public int OrderId { get; set; }
        public long ISBN { get; set; }
        public int QuantityOrdered { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}