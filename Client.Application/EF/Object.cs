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
    
    public partial class Object
    {
        public int Object_ID { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }
        public string Corp { get; set; }
        public string Room { get; set; }
        public int Streets_ID { get; set; }
        public int Cards_ID { get; set; }
    
        public virtual Streets Streets { get; set; }
        public virtual Cards Cards { get; set; }
    }
}