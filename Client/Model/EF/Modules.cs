//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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
