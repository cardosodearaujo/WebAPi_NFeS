//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnclePhill.WebAPI_NFeS.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Services
    {
        public long ServicesId { get; set; }
        public string Unity { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public decimal IRRF { get; set; }
        public decimal PIS { get; set; }
        public decimal CSLL { get; set; }
        public decimal INSS { get; set; }
        public decimal COFINS { get; set; }
        public bool Active { get; set; }
        public System.DateTime DateInsert { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    }
}
