using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analisis_Lexico
{
    public class ComponenteLexico
    {
        public string lexema { get; set; }
        public string categoria { get; set; }
        public int numLinea { get; set; }
        public int posicionInicial { get; set; }
        public int posicionFinal { get; set; }
        public bool esPalabraReservada = false;
        public ComponenteLexico(String lexema, String categoria, bool esPalabraReservada)
        {
            this.lexema = lexema;
            this.categoria = categoria;
            this.esPalabraReservada = esPalabraReservada;
        }
        public ComponenteLexico()
        {
            this.lexema = "";
            this.categoria = "";
            this.numLinea = 0;
            this.posicionInicial = 0;
            this.posicionFinal = 0;
            this.esPalabraReservada = false;
        }
        public Object Clone()
        {
            return MemberwiseClone();
        }
    }
}