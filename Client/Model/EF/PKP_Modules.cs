//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class PKP_Modules
    {
        public int PKP_Modules_ID { get; set; }
        public int Modules_ID { get; set; }
        public int PKP_ID { get; set; }
        public int Count { get; set; }
    
        public virtual Modules Modules { get; set; }
        public virtual PKP PKP { get; set; }
    }
}
