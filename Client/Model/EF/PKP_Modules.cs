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
