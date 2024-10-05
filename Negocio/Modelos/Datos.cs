using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
      public class Datos
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int price { get; set; }

        public static List<Datos> datosList = new List<Datos>
        {
            new Datos {Id=1,title="Mesa",price=10000},
            new Datos {Id=2,title="Silla",price=6500}
        };
    }
}
