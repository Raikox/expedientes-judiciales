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
    
    public partial class personal_prl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public personal_prl()
        {
            this.expediente_ambiente_ea = new HashSet<expediente_ambiente_ea>();
            this.expediente_estado_ee = new HashSet<expediente_estado_ee>();
            this.sesion_ssn = new HashSet<sesion_ssn>();
        }
    
        public int prl_id { get; set; }
        public string prl_apellidos { get; set; }
        public string prl_nombres { get; set; }
        public string prl_dni { get; set; }
        public string prl_celular { get; set; }
        public string prl_correo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_ambiente_ea> expediente_ambiente_ea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expediente_estado_ee> expediente_estado_ee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sesion_ssn> sesion_ssn { get; set; }
    }
}