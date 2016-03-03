using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Análisis_Léxico
{
    class ProgramaFuente
    {
        List<Linea> listaLineas;
        private static ProgramaFuente INSTANCIA = new ProgramaFuente();
        private ProgramaFuente()
        {
        }
        public static ProgramaFuente obtenerInstancia()
        {
            return INSTANCIA;
        }
        public List<Linea> getLineas()
        {
            return this.listaLineas;
        }
        public void setLineas(List<Linea> listaLineas)
        {
            this.listaLineas = listaLineas;
        }
        public Linea getLinea(int n)
        {
            if (n < listaLineas.Count)
            {
                return listaLineas[n];
            }
            return null;
        }

    }
}
