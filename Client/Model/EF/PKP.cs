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
    
    public partial class PKP
    {
        public PKP()
        {
            this.Cards = new HashSet<Cards>();
        }
    
        public int PKP_ID { get; set; }
        public int Name { get; set; }
        public string Serial { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[] Modules { get; set; }
        public System.DateTime Date { get; set; }
        public double Amount { get; set; }
    
        public virtual ICollection<Cards> Cards { get; set; }
    }
}
