//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class juzgado_jdo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public juzgado_jdo()
        {
            this.expediente_exp = new HashSet<expediente_exp>();
        }
    
        public int jdo_id { get; set; }
        public string jdo_nombre { get; set; }
        public int cdd_id { get; set; }
    
        public virtual ciudad_cdd ciudad_cdd { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_exp> expediente_exp { get; set; }
    }
}
