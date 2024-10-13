using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public static class ProductosAPI
    {
        public static List<Datos> datosList = new List<Datos>()
        {
            new Datos{Id =1,title="Mesa",price =200 },
            new Datos{Id =2,title="Silla",price =100 }
        };
    }
}
