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
    
    public partial class expediente_exp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public expediente_exp()
        {
            this.expediente_ambiente_ea = new HashSet<expediente_ambiente_ea>();
            this.expediente_aval_ea = new HashSet<expediente_aval_ea>();
            this.expediente_demandado_ed = new HashSet<expediente_demandado_ed>();
            this.expediente_estado_ee = new HashSet<expediente_estado_ee>();
        }
    
        public int exp_id { get; set; }
        public string exp_numero { get; set; }
        public int exp_orden { get; set; }
        public int rto_id { get; set; }
        public int mra_id { get; set; }
        public int jdo_id { get; set; }
        public int cja_id { get; set; }
    
        public virtual caja_cja caja_cja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_ambiente_ea> expediente_ambiente_ea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_aval_ea> expediente_aval_ea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_demandado_ed> expediente_demandado_ed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_estado_ee> expediente_estado_ee { get; set; }
        public virtual juzgado_jdo juzgado_jdo { get; set; }
        public virtual materia_mra materia_mra { get; set; }
        public virtual registro_rto registro_rto { get; set; }
    }
}
