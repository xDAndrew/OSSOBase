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
    
    public partial class Modules
    {
        public Modules()
        {
            this.PKP_Modules = new HashSet<PKP_Modules>();
        }
    
        public int Modules_ID { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public double UUCount { get; set; }
    
        public virtual ICollection<PKP_Modules> PKP_Modules { get; set; }
    }
}
