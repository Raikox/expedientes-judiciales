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
    
    public partial class sesion_ssn
    {
        public int ssn_id { get; set; }
        public string ssn_usuario { get; set; }
        public string ssn_password { get; set; }
        public string ssn_tipo { get; set; }
        public int prl_id { get; set; }
    
        public virtual personal_prl personal_prl { get; set; }
    }
}