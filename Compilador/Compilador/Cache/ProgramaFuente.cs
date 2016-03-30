using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Cache
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
            if (n < listaLineas.Count+1)
            {
                return listaLineas[n-1];
            }
            return null;
        }

    }
}
