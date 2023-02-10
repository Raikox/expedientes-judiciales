using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class expedientes
    {
        public int exp_id { get; set; }
        public int rto_id { get; set; }
        public int mra_id { get; set; }
        public int jdo_id { get; set; }
        public int cja_id { get; set; }
        public string exp_numero { get; set; }
        public string demandante { get; set; } //demandante puede ser 3 tipos
        public string mra_nombre { get; set; }
        public string jdo_nombre { get; set; }
        public int exp_orden { get; set; }
        public int cja_numero { get; set; }
        public int cli_id { get; set; } //cliente (entidad financiera)
    }
}
