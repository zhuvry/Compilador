using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.Analisis_Lexico;

namespace Compilador.Tabla_de_simbolos
{
    class TablaDeSimbolos
    {
        private static TablaDeSimbolos INSTANCIA =new TablaDeSimbolos();
        private Dictionary<String, List<ComponenteLexico>> mapaSimbolos = new Dictionary<string, List<ComponenteLexico>>();
        private TablaDeSimbolos() 
        {
        }
        public static TablaDeSimbolos obtenerInstancia()
        {
            return INSTANCIA;
        }
        public void adicionarSimbolo(ComponenteLexico componente)
        {
            if (componente != null)
            {
                if (mapaSimbolos.ContainsKey(componente.lexema))
                {
                    mapaSimbolos[componente.lexema].Add(componente);
                }
                else
                {
                    List<ComponenteLexico> lista = new List<ComponenteLexico>();
                    lista.Add(componente);
                    mapaSimbolos.Add(componente.lexema, lista);
                }
            }
        }
        public List<ComponenteLexico> obtenerSimbolos(String clave)
        {
            List<ComponenteLexico> listaRetorno = mapaSimbolos[clave];
            return listaRetorno;
        }
        public List<ComponenteLexico> obtenerTablaSimbolos()
        {
            return mapaSimbolos.Values.ToList().SelectMany(C=>C).ToList();
        }
    }
}
