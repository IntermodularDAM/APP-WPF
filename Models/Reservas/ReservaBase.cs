using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas_Final.Models.Reservas
{
    public class ReservaBase
    {

        public string _id { get; set; }
        public string id_usu { get; set; }
        public string id_hab { get; set; }
        public string fecha_check_in { get; set; }
        public string fecha_check_out { get; set; }
        public string estado_reserva { get; set; }
    }
}
