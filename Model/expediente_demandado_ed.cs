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
    
    public partial class expediente_demandado_ed
    {
        public int ed_id { get; set; }
        public int exp_id { get; set; }
        public int rto_id { get; set; }
    
        public virtual expediente_exp expediente_exp { get; set; }
        public virtual registro_rto registro_rto { get; set; }
    }
}