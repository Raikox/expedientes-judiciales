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
    
    public partial class expediente_ambiente_ea
    {
        public int ea_id { get; set; }
        public System.DateTime ea_fecha { get; set; }
        public string ea_motivo { get; set; }
        public int exp_id { get; set; }
        public int abt_id { get; set; }
        public int prl_id { get; set; }
    
        public virtual ambiente_abt ambiente_abt { get; set; }
        public virtual expediente_exp expediente_exp { get; set; }
        public virtual personal_prl personal_prl { get; set; }
    }
}