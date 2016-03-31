using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Manejador_de_errores
{
    public class Error
    {
        public string valorRecibido { get; set; }
        public string descripcionError { get; set; }
        public string valorEsperado { get; set; }
        public string tipoError { get; set; }
        public int numLinea { get; set; }
        public int posicionInicial { get; set; }
        public int posicionFinal { get; set; }
        
    }
}
